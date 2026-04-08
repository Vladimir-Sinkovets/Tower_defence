using Assets.Game.Scripts.Common;
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
            _data.View.PlayAttackAnimation();
        }

        public override void Exit()
        {
        }

        public override void Update() { }
    }
}