using Assets.Game.Scripts.Arena.ArenaEnemyStates;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services.Net;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ArenaEnemy : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private ArenaEnemyConfig _enemyConfig;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ArenaEnemyView _view;
        
        private IPlayerAccessor _playerAccessor;
        private Registry<ArenaEnemy> _enemyRegistry;
        private PhotonCallbacks _photonCallbacks;
        
        private StateMachine _stateMachine;
        private ArenaEnemyStateMachineData _data;
        private IEnemyDeathHandler _enemyDeathHandler;

        public Health Health { get; private set; }

        [Inject]
        public void Construct(
            IPlayerAccessor playerAccessor,
            Registry<ArenaEnemy> enemyRegistry,
            PhotonCallbacks photonCallbacks,
            IEnemyDeathHandler enemyDeathHandler)
        {
            _playerAccessor = playerAccessor;
            _enemyRegistry = enemyRegistry;
            _photonCallbacks = photonCallbacks;
            _enemyDeathHandler = enemyDeathHandler;
            
            Init();
        }

        public void Init()
        {
            _photonCallbacks.MasterClientSwitched += MasterClientSwitchedHandler;
            
            _enemyRegistry.Register(this);
            
            Health = new Health(_enemyConfig.Hp);
            
            _data = new ArenaEnemyStateMachineData()
            {
                Config = _enemyConfig,
                NavMeshAgent = _navMeshAgent,
                View = _view,
                Enemy = this,
            };
            
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new ArenaEnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new ArenaEnemyChaseState(_stateMachine, _data));
            _stateMachine.AddState(new ArenaEnemyDeathState(_stateMachine, _data));
            
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            SetTarget();
            
            _stateMachine.SetStartState<ArenaEnemyChaseState>();
        }

        private void MasterClientSwitchedHandler(Photon.Realtime.Player obj)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            SetTarget();
            
            _stateMachine.SetStartState<ArenaEnemyChaseState>();
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            _stateMachine.Update();
        }

        private void SetTarget()
        {
            if (_data.Target != null)
                return;

            _data.Target = GetNearestTarget();
        }

        private ArenaPlayer GetNearestTarget()
        {
            var distance = float.MaxValue;

            ArenaPlayer nearestTarget = null;
            
            foreach (var player in _playerAccessor.Players)
            {
                if (Vector3.Distance(player.Position, transform.position) <= distance)
                {
                    nearestTarget = player;
                    
                    distance = Vector3.Distance(player.Position, transform.position);
                }
            }
            
            return nearestTarget;
        }

        public void ApplyDamage(int damage, int playerViewId) => _photonView.RPC(nameof(TakeDamage), RpcTarget.All, damage, playerViewId);

        [PunRPC]
        private void TakeDamage(int damage, int playerViewId)
        {
            Health.ApplyDamage(damage);

            if (Health.IsDead)
            {
                _enemyDeathHandler.EnemyDiedHandler(playerViewId);
            }
        }
    }
}