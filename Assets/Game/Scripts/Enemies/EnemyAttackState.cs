using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Enemies
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
            _data.Target.GetDamage(_data.Config.Damage);

            StateSwitcher.SwitchState<EnemyIdleState>();
        }

        public override void Exit() { }

        public override void Update() { }
    }
}