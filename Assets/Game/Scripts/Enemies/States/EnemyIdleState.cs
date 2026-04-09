using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyIdleState : State
    {
        private readonly EnemyStateMachineData _data;

        private float _time;

        public EnemyIdleState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            _time = 0.0f;

            _data.View.PlayIdleAnimation();

            _data.Enemy.Health.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            _time += Time.deltaTime;

            if (Vector3.Distance(_data.Transform.position, _data.Target.transform.position) > _data.Config.AttackRange)
            {
                StateSwitcher.SwitchState<EnemyRunState>();
            }
            else if (_time >= _data.Config.IntervalBetweenAttacks)
            {
                StateSwitcher.SwitchState<EnemyAttackState>();
            }
        }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<EnemyDeathState>();
        }
    }
}