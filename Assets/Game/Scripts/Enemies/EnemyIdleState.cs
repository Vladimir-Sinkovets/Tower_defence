using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
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
        }

        public override void Exit() { }

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
    }
}