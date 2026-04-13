using Assets.Game.Scripts.Common.UniversalStateMachine;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyAttackState : State
    {
        private readonly EnemyStateMachineData _data;

        private bool _isAttacking;
        private float _nextAttackTime;

        public SimpleEnemyAttackState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher) => _data = data;

        public override void Enter() => _data.Enemy.Health.OnDied += OnEnemyDied;

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            if (_isAttacking)
                return;

            if (_data.Enemy.IsActive == false)
            {
                StateSwitcher.SwitchState<EnemyIdleState>();
                return;
            }

            if (Vector3.Distance(_data.Transform.position, _data.Target.transform.position) > _data.Config.AttackRange)
            {
                StateSwitcher.SwitchState<EnemyRunState>();
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
            if (_data.Enemy.Health.IsDead == true || _data.Enemy.IsActive == false)
                return;

            _data.Target.ApplyDamage(_data.Config.Damage);

            _isAttacking = false;

            _nextAttackTime = Time.time + _data.Config.IntervalBetweenAttacks;
        }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<SimpleEnemyDeathState>();
        }
    }
}