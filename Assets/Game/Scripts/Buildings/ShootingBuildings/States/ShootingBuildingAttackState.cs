using Assets.Game.Scripts.Common.UniversalStateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingBuildingAttackState : State
    {
        private readonly ShootingBuildingStateMachineData _data;
        private readonly ShootingExecutor _shootingExecutor;

        public ShootingBuildingAttackState(
            ShootingBuildingStateMachineData data,
            IStateSwitcher stateSwitcher,
            ShootingExecutor shootingExecutor) : base(stateSwitcher)
        {
            _data = data;
            _shootingExecutor = shootingExecutor;

            _shootingExecutor.Init(_data);
        }

        public override void Enter()
        {
            _data.ShootingBuilding.OnStopped += OnStoppedHandler;

            if (_data.CurrentTarget != null)
                _data.CurrentTarget.OnDied += OnCurrentTargetDiedHandler;
        }

        public override void Exit()
        {
            _data.ShootingBuilding.OnStopped -= OnStoppedHandler;

            if (_data.CurrentTarget != null)
            {
                _data.CurrentTarget.OnDied -= OnCurrentTargetDiedHandler;
                _data.CurrentTarget = null;
            }
        }

        public override void Update()
        {
            if (Vector3.Distance(_data.CurrentTarget.transform.position, _data.Transform.position) > _data.Config.AttackRadius)
            {
                StateSwitcher.SwitchState<ShootingBuildingWaitState>();
                
                return;
            }

            RotateWeapon();

            Attack();
        }
        
        private void Attack() => _shootingExecutor.Attack().Forget();


        private void RotateWeapon()
        {
            if (_data.CurrentTarget == null)
                return;

            var direction = _data.CurrentTarget.transform.position - _data.WeaponRoot.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                var targetRotation = Quaternion.LookRotation(direction);
                _data.WeaponRoot.rotation = Quaternion.RotateTowards(
                    _data.WeaponRoot.rotation,
                    targetRotation,
                    _data.Config.RotationSpeed * Time.deltaTime
                );
            }
        }

        private void OnCurrentTargetDiedHandler() => StateSwitcher.SwitchState<ShootingBuildingWaitState>();
        private void OnStoppedHandler() => StateSwitcher.SwitchState<ShootingBuildingStopState>();
    }
}