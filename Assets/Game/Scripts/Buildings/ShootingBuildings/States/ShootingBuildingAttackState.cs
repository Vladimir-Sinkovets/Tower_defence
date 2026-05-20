using System.Threading;
using Assets.Game.Scripts.Buildings.Implementations;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingBuildingAttackState : State
    {
        private readonly ShootingBuildingStateMachineData _data;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVFXFactory _vfxFactory;

        private CancellationTokenSource _shootCts;
        private float _nextShootTime;

        public ShootingBuildingAttackState(ShootingBuildingStateMachineData data, IStateSwitcher stateSwitcher, IProjectileFactory projectileFactory, IVFXFactory vfxFactory) : base(stateSwitcher)
        {
            _data = data;
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
        }

        public override void Enter()
        {
            _data.CurrentTarget.Health.OnDied += OnCurrentTargetDiedHandler;
            _data.ShootingBuilding.OnStopped += OnStoppedHandler;

            _nextShootTime = Time.time + _data.Config.AttackInterval;
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = new CancellationTokenSource();
        }

        public override void Exit()
        {
            _data.ShootingBuilding.OnStopped -= OnStoppedHandler;

            if (_data.CurrentTarget == null) return;
            
            _data.CurrentTarget.Health.OnDied -= OnCurrentTargetDiedHandler;
            _data.CurrentTarget = null;
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

        private void OnStoppedHandler() => StateSwitcher.SwitchState<ShootingBuildingStopState>();
        
        private void Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            _nextShootTime = Time.time + _data.Config.AttackInterval;

            Shoot(_shootCts.Token).Forget();
        }

        private async UniTask Shoot(CancellationToken ct)
        {
            if (_data.PreShootAnimation != null)
                await _data.PreShootAnimation.PlayBeforeAttackAnimation(ct);

            if (_data.Config.ShootVFX != null)
                _vfxFactory.Create(_data.Config.ShootVFX, _data.ProjectileStartPosition.position);

            _projectileFactory.Create(
                _data.Config.ProjectilePrefab,
                new  ProjectileData
                {
                    Position = _data.ProjectileStartPosition.position,
                    Target = _data.CurrentTarget,
                    Damage = _data.Config.Damage,
                    ProjectileSpeed = _data.Config.ProjectileSpeed,
                    ArcHeight = _data.Config.ArcHeight,
                    HitVFXPrefab = _data.Config.HitVFX,
                });
        }
        
        private void RotateWeapon()
        {
            if (_data.CurrentTarget == null) return;

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
    }
}