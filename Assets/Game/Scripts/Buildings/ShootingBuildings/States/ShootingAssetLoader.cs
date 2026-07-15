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
        
        private bool _assetsLoaded;
        private bool _isLoading;
        private UniTask _loadAssetsTask;
        
        public ShootingAssetLoader(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public async UniTask EnsureAssetsLoaded(
            AssetReference shootVFXPrefab,
            AssetReference projectilePrefab,
            AssetReference hitVFXPrefab,
            CancellationToken ct = default)
        {
            if (_assetsLoaded)
                return;
            
            if (_isLoading)
            {
                await _loadAssetsTask;
                return;
            }
            
            _shootVFXPrefab = shootVFXPrefab;
            _projectilePrefab = projectilePrefab;
            _hitVFXPrefab = hitVFXPrefab;
            
            _isLoading = true;
            
            _loadAssetsTask = LoadAssetsAsync(ct);
            
            try
            {
                await _loadAssetsTask;
            }
            finally
            {
                _isLoading = false;
            }
        }
        
        private async UniTask LoadAssetsAsync(CancellationToken ct = default)
        {
            try
            {
                GameObject shootVFX = null;
                GameObject projectile = null;
                GameObject hitVFX = null;
                
                if (_shootVFXPrefab.RuntimeKeyIsValid())
                    shootVFX = await _assetProvider.Load<GameObject>(_shootVFXPrefab, ct);
                if (_projectilePrefab.RuntimeKeyIsValid())
                    projectile = await _assetProvider.Load<GameObject>(_projectilePrefab, ct);
                if (_hitVFXPrefab.RuntimeKeyIsValid())
                    hitVFX = await _assetProvider.Load<GameObject>(_hitVFXPrefab, ct);

                CachedShootVFXPrefab = shootVFX?.GetComponent<ParticleSystem>();
                CachedProjectilePrefab = projectile?.GetComponent<Projectile>();
                CachedHitVFXPrefab = hitVFX?.GetComponent<ParticleSystem>();

                _assetsLoaded = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load shooting assets: {ex}");
            }
        }
        
        public void UnloadAssets()
        {
            if (!_assetsLoaded)
                return;
            
            _assetProvider.Unload(_shootVFXPrefab);
            _assetProvider.Unload(_projectilePrefab);
            _assetProvider.Unload(_hitVFXPrefab);
            _assetsLoaded = false;
            CachedShootVFXPrefab = null;
            CachedProjectilePrefab = null;
            CachedHitVFXPrefab = null;
        }
    }
}