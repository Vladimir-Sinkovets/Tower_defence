using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyDeathState : State
    {
        private readonly EnemyStateMachineData _data;

        public EnemyDeathState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            _data.View.PlayDeathAnimation();
            _data.View.DisableCanvas();
            _data.View.RemoveModel();

            _data.NavMeshAgent.enabled = false;

            Object.Destroy(_data.Enemy.gameObject, 3.0f);
        }

        public override void Exit()
        {
            _data.View.EnableCanvas();
        }

        public override void Update() { }
    }
}