using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyChaseState : State
    {
        private readonly ArenaEnemyStateMachineData _data;

        public ArenaEnemyChaseState(IStateSwitcher stateSwitcher, ArenaEnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }
        
        public override void Enter()
        {
            _data.View.PlayWalkAnimation();

            _data.Enemy.Health.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            _data.Enemy.Health.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            if (_data.Target == null)
            {
                _data.Enemy.SetTarget();
                return;
            }
            
            var direction = _data.Target.transform.position - _data.Enemy.transform.position;
            direction.y = 0f;

            var distance = direction.magnitude;

            if (distance <= _data.Config.AttackRange)
            {
                StateSwitcher.SwitchState<ArenaEnemyAttackState>();
            }

            if (direction.sqrMagnitude > 0.001f)
            {
                var targetRotation = Quaternion.LookRotation(direction);

                _data.Enemy.transform.rotation = Quaternion.Slerp(
                    _data.Enemy.transform.rotation,
                    targetRotation,
                    _data.Config.RotationSpeed * Time.deltaTime
                );

                _data.Enemy.transform.position += _data.Enemy.transform.forward *
                    (_data.Config.Speed * Time.deltaTime);
            }
        }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<ArenaEnemyDeathState>();
        }
    }
}