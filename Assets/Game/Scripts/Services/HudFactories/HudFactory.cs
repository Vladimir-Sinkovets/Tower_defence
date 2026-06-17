using System;
using System.Threading;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Currency;
using Assets.Game.Scripts.UI.HealthBar;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.Game.Scripts.Services.HudFactories
{
    public class HudFactory : IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly AssetReference _hudPrefab;
        private readonly IAssetProvider _assetProvider;

        private CurrencyPresenter _currencyPresenter;
        private HealthBarPresenter _castleHealthPresenter;
        
        private CancellationTokenSource _startGameCts;

        public HudFactory(IInstantiator instantiator, AssetReference hudPrefab, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _hudPrefab = hudPrefab;
            _assetProvider = assetProvider;
        }

        public async UniTask CreateHUD(Health castleHealth)
        {
            var prefab = await _assetProvider.Load<GameObject>(_hudPrefab);
            
            var hud = _instantiator.InstantiatePrefabForComponent<HUD>(prefab.GetComponent<HUD>());

            _currencyPresenter = _instantiator.Instantiate<CurrencyPresenter>(new object[] { hud.CurrencyView });
            _castleHealthPresenter = _instantiator.Instantiate<HealthBarPresenter>(new object[] { hud.HealthBarView, castleHealth });
        }

        public void Dispose()
        {
            _currencyPresenter?.Dispose();
            _castleHealthPresenter?.Dispose();
        }
    }
}