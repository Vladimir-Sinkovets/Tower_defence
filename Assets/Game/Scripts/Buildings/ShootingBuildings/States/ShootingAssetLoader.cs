using System;
using System.Threading;
using Assets.Game.Scripts.Services.AssetProviders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingAssetLoader
    {
        private readonly IAssetProvider _assetProvider;
        public ParticleSystem CachedShootVFXPrefab { get; private set; }
        public Projectile CachedProjectilePrefab { get; private set; }
        public ParticleSystem CachedHitVFXPrefab { get; private set; }
        
        private AssetReference _shootVFXPrefab;
        private AssetReference _projectilePrefab;
        private AssetReference _hitVFXPrefab;

        private UniTask _loadAssetsTask;
        
        public bool AssetsLoaded { get; private set; }
        
        public ShootingAssetLoader(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public async UniTask EnsureAssetsLoadedAsync(
            AssetReference shootVFXPrefab,
            AssetReference projectilePrefab,
            AssetReference hitVFXPrefab,
            CancellationToken ct = default)
        {
            if (AssetsLoaded)
                return;
            
            if (_loadAssetsTask.Status == UniTaskStatus.Pending)
            {
                await _loadAssetsTask;
                return;
            }
            
            _shootVFXPrefab = shootVFXPrefab;
            _projectilePrefab = projectilePrefab;
            _hitVFXPrefab = hitVFXPrefab;

            try
            {
                _loadAssetsTask = LoadAssetsAsync(ct);
                
                await _loadAssetsTask;
            }
            catch (Exception ex)
            {
                CachedShootVFXPrefab = null;
                CachedProjectilePrefab = null;
                CachedHitVFXPrefab = null;
                
                _assetProvider.Unload(_shootVFXPrefab);
                _assetProvider.Unload(_projectilePrefab);
                _assetProvider.Unload(_hitVFXPrefab);

                _loadAssetsTask = default;
                
                Debug.LogError($"Failed to load shooting assets: {ex}");

                throw;
            }
        }

        private async UniTask LoadAssetsAsync(CancellationToken ct = default)
        {
            if (_shootVFXPrefab.RuntimeKeyIsValid())
            {
                var shootVFX = await _assetProvider.Load<GameObject>(_shootVFXPrefab, ct);
                CachedShootVFXPrefab = shootVFX.GetComponent<ParticleSystem>();
            }

            if (_projectilePrefab.RuntimeKeyIsValid())
            {
                var projectile = await _assetProvider.Load<GameObject>(_projectilePrefab, ct);
                CachedProjectilePrefab = projectile.GetComponent<Projectile>();
            }

            if (_hitVFXPrefab.RuntimeKeyIsValid())
            {
                var hitVFX = await _assetProvider.Load<GameObject>(_hitVFXPrefab, ct);
                CachedHitVFXPrefab = hitVFX.GetComponent<ParticleSystem>();
            }

            AssetsLoaded = true;
        }
        
        public void UnloadAssets()
        {
            if (!AssetsLoaded)
                return;
            
            _assetProvider.Unload(_shootVFXPrefab);
            _assetProvider.Unload(_projectilePrefab);
            _assetProvider.Unload(_hitVFXPrefab);
            AssetsLoaded = false;
            CachedShootVFXPrefab = null;
            CachedProjectilePrefab = null;
            CachedHitVFXPrefab = null;
        }
    }
}