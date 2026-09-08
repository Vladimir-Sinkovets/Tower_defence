using System;
using System.Threading;
using Assets.Game.Scripts.Arena.Buildings.Implementations;
using Assets.Game.Scripts.Arena.Buildings.Interfaces;
using Assets.Game.Scripts.Arena.Buildings.ShootingBuildings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Buildings.States
{
    public class ShootingExecutor : IDisposable
    {
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVFXFactory _vfxFactory;
        private readonly IBuildingUpgradeApplier _buildingUpgradeApplier;

        private ShootingBuildingStateMachineData _data;
        
        private CancellationTokenSource _shootCts;
        private float _nextShootTime;

        public ShootingExecutor(
            IProjectileFactory projectileFactory,
            IVFXFactory vfxFactory,
            IBuildingUpgradeApplier buildingUpgradeApplier)
        {
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
        }

        public void Init(ShootingBuildingStateMachineData data)
        {
            _data = data;

            SetNextShootTime();
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = new CancellationTokenSource();
        }

        public async UniTask Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            SetNextShootTime();

            await ShootAsync(_shootCts.Token);
        }


        private void SetNextShootTime() => 
            _nextShootTime = Time.time + _buildingUpgradeApplier.ApplyBuildingAttackSpeedUpgrade(_data.Config.AttackInterval);

        private async UniTask ShootAsync(CancellationToken ct)
        {
            if (_data.PreShootAnimation != null)
                await _data.PreShootAnimation.PlayBeforeAttackAnimationAsync(ct);
            
            if (_data.Config.ShootVFXPrefab != null)
                _vfxFactory.Create(_data.Config.ShootVFXPrefab, _data.ProjectileStartPosition.position);
            
            _projectileFactory.Create(
                _data.Config.ProjectilePrefabName,
                new ProjectileData
                {
                    Position = _data.ProjectileStartPosition.position,
                    Target = _data.CurrentTarget,
                    Damage = _buildingUpgradeApplier.ApplyBuildingDamageUpgrade(_data.Config.Damage),
                    ProjectileSpeed = _data.Config.ProjectileSpeed,
                    ArcHeight = _data.Config.ArcHeight,
                    HitVFXPrefab = _data.Config.HitVFXPrefab,
                });
        }

        public void Dispose()
        {
            _shootCts?.Cancel();
            _shootCts?.Dispose();
        }
    }
}