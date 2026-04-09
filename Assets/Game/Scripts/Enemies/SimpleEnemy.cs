using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemies
{
    public class SimpleEnemy : Enemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        public void Init(Health _target, SimpleEnemyFactory config)
        {
            _stateMachine = new StateMachine();
            _data = new EnemyStateMachineData()
            {
                NavMeshAgent = _navMeshAgent,
                Target = _target,
                Transform = transform,
                View = _enemyView,
                Config = config,
                Health = Health,
            };

            _navMeshAgent.speed = config.Speed;

            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyDeathState(_stateMachine, _data));

            _stateMachine.SetStartState<EnemyRunState>();
        }

        private void Update() => _stateMachine.Update();

        private void OnDestroy() => _stateMachine.Dispose();
    }
}