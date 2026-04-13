using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyDeathState : State
    {
        private readonly EnemyStateMachineData _data;

        public SimpleEnemyDeathState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            _data.View.PlayDeathAnimation();
            _data.View.DisableCanvas();
            _data.View.RemoveModel();

            _data.NavMeshAgent.enabled = false;

            Object.Destroy(_data.Enemy.gameObject, 3.0f);
        }

        public override void Exit() { }

        public override void Update() { }
    }
}