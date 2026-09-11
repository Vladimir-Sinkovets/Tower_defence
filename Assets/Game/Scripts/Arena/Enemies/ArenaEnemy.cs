using Assets.Game.Scripts.Arena.ArenaEnemyStates;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
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
    public class ArenaEnemy : MonoBehaviour, IPunObservable
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private ArenaEnemyConfig _enemyConfig;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ArenaEnemyView _view;
        
        private Registry<ArenaEnemy> _enemyRegistry;
        private PhotonCallbacks _photonCallbacks;
        private IEnemyDeathHandler _enemyDeathHandler;
        private IPlayerAccessor _playerAccessor;
        
        private StateMachine _stateMachine;
        private ArenaEnemyStateMachineData _data;
        private int _targetViewId;

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

        public void SetTarget()
        {
            var viewId = PhotonView.Find(_targetViewId);

            if (viewId != null)
            {
                _data.Target = viewId.GetComponent<ArenaPlayer>();
                
                _targetViewId = _data.Target.PhotonView.ViewID;

                if (_data.Target != null)
                    return;
            }

            _data.Target = _playerAccessor.GetNearestTarget(transform.position);
            
            _targetViewId = _data.Target.PhotonView.ViewID;
        }

        private void Init()
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
            
            _stateMachine?.Update();
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_targetViewId);
            }
            else
            {
                _targetViewId = (int)stream.ReceiveNext();
            }
        }

        private void OnDestroy() => _enemyRegistry.Unregister(this);
    }
}