using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Enemies.States
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

            _data.Enemy.Health.OnDied += OnEnemyDied;
        }

        private void AttackAnimationEventHandler()
        {
            _data.Target.GetDamage(_data.Config.Damage);

            if (_data.Enemy.Health.IsDied)
                return;

            StateSwitcher.SwitchState<EnemyIdleState>();
        }

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update() { }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<EnemyDeathState>();
        }
    }
}