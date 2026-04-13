using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyIdleState : State
    {
        private readonly EnemyStateMachineData _data;

        public EnemyIdleState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            _data.View.PlayIdleAnimation();

            _data.Enemy.Health.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update() { }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<SimpleEnemyDeathState>();
        }
    }
}