using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;
using State = Assets.Game.Scripts.Common.UniversalStateMachine.State;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyAttackState : State
    {
        private readonly ArenaEnemyStateMachineData _data;
        
        private bool _isAttacking;
        private float _nextAttackTime;

        public ArenaEnemyAttackState(IStateSwitcher stateSwitcher, ArenaEnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            _data.Enemy.Health.OnDied += OnEnemyDied;
            _data.View.OnAttacked += AttackAnimationEventHandler;
        }

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
            _data.View.OnAttacked -= AttackAnimationEventHandler;
        }

        public override void Update()
        {
            if (_isAttacking)
                return;

            if (_data.Target == null)
            {
                _data.Enemy.SetTarget();
                return;
            }
            
            if (!IsInAttackRange())
            {
                StateSwitcher.SwitchState<ArenaEnemyChaseState>();
                return;
            }

            if (_nextAttackTime > Time.time)
                return;

            Attack();
        }

        private void Attack()
        {
            _isAttacking = true;

            _data.View.PlayAttackAnimation();
        }

        private void AttackAnimationEventHandler()
        {
            if (_data.Enemy.Health.IsDead)
                return;

            _data.Target.ApplyDamage(_data.Config.Damage);

            _isAttacking = false;
            
            _nextAttackTime = Time.time + _data.Config.IntervalBetweenAttacks;
        }

        private void OnEnemyDied() => StateSwitcher.SwitchState<ArenaEnemyDeathState>();

        private bool IsInAttackRange()
        {
            var attackRange = _data.Config.AttackRange;
            var distance = Vector3.Distance(_data.Enemy.transform.position, _data.Target.Position);

            return distance <= attackRange;
        }
    }
}