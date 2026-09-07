using System;
using Assets.Game.Scripts.Arena.ArenaEnemyStates;
using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ArenaEnemy : MonoBehaviour
    {
        public event Action OnDied;
        
        [SerializeField] private ArenaEnemyConfig _enemyConfig;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ArenaEnemyView _view;
        
        private IPlayerAccessor _playerAccessor;
        
        private StateMachine _stateMachine;
        private ArenaEnemyStateMachineData _data;

        public bool IsDead => false;

        [Inject]
        public void Construct(IPlayerAccessor playerAccessor) => _playerAccessor = playerAccessor;

        public void Init()
        {
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
            _stateMachine.AddState(new ArenaEnemyDeathState(_stateMachine));
            
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
    }
}