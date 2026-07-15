using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyRunState : State
    {
        private readonly SimpleEnemyStateMachineData _data;

        public SimpleEnemyRunState(IStateSwitcher stateSwitcher, SimpleEnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            if (_data.NavMeshAgent != null && _data.NavMeshAgent.isOnNavMesh)
                _data.NavMeshAgent.isStopped = false;
            
            _data.View.PlayWalkAnimation();

            _data.Enemy.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            if (_data.NavMeshAgent != null && _data.NavMeshAgent.isOnNavMesh)
                _data.NavMeshAgent.isStopped = true;

            _data.Enemy.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            _data.NavMeshAgent.SetDestination(_data.TargetTransform.position);

            if (!_data.Enemy.IsActive)
            {
                StateSwitcher.SwitchState<SimpleEnemyIdleState>();
            }
            else if (Vector3.Distance(_data.Transform.position, _data.TargetTransform.position) <= _data.Settings.AttackRange)
            {
                StateSwitcher.SwitchState<SimpleEnemyAttackState>();
            }
        }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<SimpleEnemyDeathState>();
        }
    }
}