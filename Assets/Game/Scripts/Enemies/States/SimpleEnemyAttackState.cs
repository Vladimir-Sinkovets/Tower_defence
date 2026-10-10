using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyAttackState : State
    {
        private readonly SimpleEnemyStateMachineData _data;

        private bool _isAttacking;
        private float _nextAttackTime;

        public SimpleEnemyAttackState(IStateSwitcher stateSwitcher, SimpleEnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter()
        {
            _data.Enemy.OnDied += OnEnemyDied;
            _data.Enemy.Deactivated += OnDeactivatedHandler;
        }

        public override void Exit()
        {
            _data.Enemy.OnDied -= OnEnemyDied;
            _data.Enemy.Deactivated -= OnDeactivatedHandler;
        }

        public override void Update()
        {
            if (_isAttacking)
                return;

            if (Vector3.Distance(_data.Transform.position, _data.TargetTransform.position) > _data.Settings.AttackRange)
            {
                StateSwitcher.SwitchState<SimpleEnemyRunState>();
                return;
            }

            if (_nextAttackTime > Time.time)
                return;

            Attack();
        }

        private void Attack()
        {
            _isAttacking = true;

            _data.View.PlayAttackAnimation(AttackAnimationEventHandler);
        }

        private void AttackAnimationEventHandler()
        {
            if (_data.Enemy.IsDead)
                return;

            _data.TargetHealth.ApplyDamage(_data.Settings.Damage);

            _isAttacking = false;

            _nextAttackTime = Time.time + _data.Settings.IntervalBetweenAttacks;
        }

        private void OnDeactivatedHandler() => StateSwitcher.SwitchState<SimpleEnemyIdleState>();

        private void OnEnemyDied(Enemy _) => StateSwitcher.SwitchState<SimpleEnemyDeathState>();
    }
}