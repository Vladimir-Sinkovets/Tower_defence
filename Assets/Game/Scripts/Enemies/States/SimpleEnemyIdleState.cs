using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyIdleState : State
    {
        private readonly SimpleEnemyStateMachineData _data;

        public SimpleEnemyIdleState(IStateSwitcher stateSwitcher, SimpleEnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            _data.View.PlayIdleAnimation();

            _data.Enemy.OnDied += OnEnemyDiedHandler;
            _data.Enemy.Activated += OnActivatedHandler;
        }


        public override void Exit()
        {
            _data.Enemy.OnDied -= OnEnemyDiedHandler;
            _data.Enemy.Activated -= OnActivatedHandler;
        }

        private void OnActivatedHandler() => StateSwitcher.SwitchState<SimpleEnemyRunState>();

        private void OnEnemyDiedHandler(Enemy _) => StateSwitcher.SwitchState<SimpleEnemyDeathState>();
    }
}