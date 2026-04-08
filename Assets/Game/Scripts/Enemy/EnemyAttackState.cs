using Assets.Game.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Enemy
{
    public class EnemyAttackState : State
    {
        private readonly EnemyStateMachineData _data;

        public EnemyAttackState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            _data.View.PlayAttackAnimation(AttackAnimationEventHandler);
        }

        private void AttackAnimationEventHandler()
        {
            _data.Target.GetDamage(5);

            StateSwitcher.SwitchState<EnemyIdleState>();
        }

        public override void Exit() { }

        public override void Update() { }
    }
}