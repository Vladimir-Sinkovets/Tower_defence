using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Enemies
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
        }

        public override void Exit() { }

        public override void Update() { }
    }
}