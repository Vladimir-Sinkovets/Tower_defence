using System;
using System.Threading;
using Assets.Game.Scripts.Buildings.Implementations;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingBuildingAttackState : State, IDisposable
    {
        private readonly ShootingBuildingStateMachineData _data;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVFXFactory _vfxFactory;
        private readonly IBuildingUpgradeApplier _buildingUpgradeApplier;
        private readonly IAssetProvider _assetProvider;

        private CancellationTokenSource _shootCts;
        private float _nextShootTime;
        
        private ParticleSystem _cachedShootVFXPrefab;
        private Projectile _cachedProjectilePrefab;
        private ParticleSystem _cachedHitVFXPrefab;
        
        private bool _assetsLoaded;
        private UniTask _loadAssetsTask;

        public ShootingBuildingAttackState(
            ShootingBuildingStateMachineData data,
            IStateSwitcher stateSwitcher,
            IProjectileFactory projectileFactory,
            IVFXFactory vfxFactory,
            IBuildingUpgradeApplier buildingUpgradeApplier,
            IAssetProvider assetProvider) : base(stateSwitcher)
        {
            _data = data;
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _buildingUpgradeApplier = buildingUpgradeApplier;
            _assetProvider = assetProvider;

            _loadAssetsTask = LoadAssetsAsync();
        }

        public override void Enter()
        {
            _data.CurrentTarget.OnDied += OnCurrentTargetDiedHandler;
            _data.ShootingBuilding.OnStopped += OnStoppedHandler;

            SetNextShootTime();
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = new CancellationTokenSource();
        }

        public override void Exit()
        {
            _data.ShootingBuilding.OnStopped -= OnStoppedHandler;

            if (_data.CurrentTarget == null)
                return;
            
            _data.CurrentTarget.OnDied -= OnCurrentTargetDiedHandler;
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

            SetNextShootTime();

            Shoot(_shootCts.Token).Forget();
        }

        private void SetNextShootTime()
        {
            _nextShootTime = Time.time +
                _buildingUpgradeApplier.ApplyBuildingAttackSpeedUpgrade(_data.Config.AttackInterval, _data.BuildingType);
        }

        private async UniTask Shoot(CancellationToken ct)
        {
            await EnsureAssetsLoaded(ct);
            
            if (_data.PreShootAnimation != null)
                await _data.PreShootAnimation.PlayBeforeAttackAnimation(ct);
            
            if (_cachedShootVFXPrefab != null)
                _vfxFactory.Create(_cachedShootVFXPrefab, _data.ProjectileStartPosition.position);
            
            _projectileFactory.Create(
                _cachedProjectilePrefab,
                new  ProjectileData
                {
                    Position = _data.ProjectileStartPosition.position,
                    Target = _data.CurrentTarget,
                    Damage = _buildingUpgradeApplier.ApplyBuildingDamageUpgrade(_data.Config.Damage, _data.BuildingType),
                    ProjectileSpeed = _data.Config.ProjectileSpeed,
                    ArcHeight = _data.Config.ArcHeight,
                    HitVFXPrefab = _cachedHitVFXPrefab,
                });
        }
        
        private async UniTask LoadAssetsAsync(CancellationToken ct = default)
        {
            try
            {
                GameObject shootVFX = null;
                GameObject projectile = null;
                GameObject hitVFX = null;
                
                if (_data.Config.ShootVFXPrefab.RuntimeKeyIsValid())
                    shootVFX = await _assetProvider.Load<GameObject>(_data.Config.ShootVFXPrefab, ct);
                if (_data.Config.ProjectilePrefab.RuntimeKeyIsValid())
                    projectile = await _assetProvider.Load<GameObject>(_data.Config.ProjectilePrefab, ct);
                if (_data.Config.HitVFXPrefab.RuntimeKeyIsValid())
                    hitVFX = await _assetProvider.Load<GameObject>(_data.Config.HitVFXPrefab, ct);

                _cachedShootVFXPrefab = shootVFX?.GetComponent<ParticleSystem>();
                _cachedProjectilePrefab = projectile?.GetComponent<Projectile>();
                _cachedHitVFXPrefab = hitVFX?.GetComponent<ParticleSystem>();

                _assetsLoaded = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load shooting assets: {ex}");
            }
        }

        private async UniTask EnsureAssetsLoaded(CancellationToken ct)
        {
            if (_assetsLoaded) return;

            if (_loadAssetsTask.Status != UniTaskStatus.Succeeded && 
                _loadAssetsTask.Status != UniTaskStatus.Faulted)
            {
                await _loadAssetsTask.SuppressCancellationThrow();
            }
            else
            {
                await LoadAssetsAsync(ct);
            }
        }

        
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

        public void Dispose()
        {
            _shootCts?.Dispose();        
            
            if (_assetsLoaded)
            {
                _assetProvider.Unload(_data.Config.ShootVFXPrefab);
                _assetProvider.Unload(_data.Config.ProjectilePrefab);
                _assetProvider.Unload(_data.Config.HitVFXPrefab);
                _assetsLoaded = false;
                _cachedShootVFXPrefab = null;
                _cachedProjectilePrefab = null;
                _cachedHitVFXPrefab = null;
            }
        }
    }
}