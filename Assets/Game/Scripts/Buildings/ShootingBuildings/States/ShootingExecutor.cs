using System;
using System.Threading;
using Assets.Game.Scripts.Buildings.Implementations;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingExecutor : IDisposable
    {
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVFXFactory _vfxFactory;
        private readonly IBuildingUpgradeApplier _buildingUpgradeApplier;
        private readonly ShootingAssetLoader _assetLoader;

        private ShootingBuildingStateMachineData _data;
        
        private CancellationTokenSource _shootCts;
        private float _nextShootTime;

        public ShootingExecutor(
            IProjectileFactory projectileFactory,
            IVFXFactory vfxFactory,
            IBuildingUpgradeApplier buildingUpgradeApplier,
            ShootingAssetLoader assetLoader)
        {
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
            _assetLoader = assetLoader;
        }

        public void Init(ShootingBuildingStateMachineData data)
        {
            _data = data;

            SetNextShootTime();
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = new CancellationTokenSource();

            _assetLoader.EnsureAssetsLoaded(
                _data.Config.ShootVFXPrefab, 
                _data.Config.ProjectilePrefab,
                _data.Config.HitVFXPrefab).Forget();
        }

        public async UniTask Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            SetNextShootTime();

            await Shoot(_shootCts.Token);
        }


        private void SetNextShootTime()
        {
            _nextShootTime = Time.time +
                             _buildingUpgradeApplier.ApplyBuildingAttackSpeedUpgrade(_data.Config.AttackInterval, _data.BuildingType);
        }

        private async UniTask Shoot(CancellationToken ct)
        {
            await _assetLoader.EnsureAssetsLoaded(
                _data.Config.ShootVFXPrefab, 
                _data.Config.ProjectilePrefab,
                _data.Config.HitVFXPrefab,
                ct);
            
            if (_data.PreShootAnimation != null)
                await _data.PreShootAnimation.PlayBeforeAttackAnimation(ct);
            
            if (_assetLoader.CachedShootVFXPrefab != null)
                _vfxFactory.Create(_assetLoader.CachedShootVFXPrefab, _data.ProjectileStartPosition.position);
            
            _projectileFactory.Create(
                _assetLoader.CachedProjectilePrefab,
                new  ProjectileData
                {
                    Position = _data.ProjectileStartPosition.position,
                    Target = _data.CurrentTarget,
                    Damage = _buildingUpgradeApplier.ApplyBuildingDamageUpgrade(_data.Config.Damage, _data.BuildingType),
                    ProjectileSpeed = _data.Config.ProjectileSpeed,
                    ArcHeight = _data.Config.ArcHeight,
                    HitVFXPrefab = _assetLoader.CachedHitVFXPrefab,
                });
        }

        public void Dispose()
        {
            _shootCts?.Dispose();
            
            _assetLoader.UnloadAssets();
        }
    }
}