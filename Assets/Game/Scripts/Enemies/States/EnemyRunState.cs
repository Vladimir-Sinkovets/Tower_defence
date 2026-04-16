using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyRunState : State
    {
        private readonly EnemyStateMachineData _data;

        public EnemyRunState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            _data.NavMeshAgent.SetDestination(_data.Target.transform.position);

            _data.View.PlayWalkAnimation();

            _data.Enemy.Health.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            if (_data.NavMeshAgent && _data.NavMeshAgent.isOnNavMesh)
                _data.NavMeshAgent.isStopped = true;

            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            if (!_data.Enemy.IsActive)
            {
                StateSwitcher.SwitchState<EnemyIdleState>();
            }
            else if (Vector3.Distance(_data.Transform.position, _data.Target.transform.position) <= _data.Config.AttackRange)
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