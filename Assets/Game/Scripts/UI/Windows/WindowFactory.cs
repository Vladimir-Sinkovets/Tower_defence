using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.UI.Windows.Buildings;
using Assets.Game.Scripts.UI.Windows.ContinueByAd;
using Assets.Game.Scripts.UI.Windows.EndGame;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.Game.Scripts.UI.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<WindowType, Func<UniTask<IWindowPresenter>>> _factoryDelegates;

        public WindowFactory(WindowViewsConfig config, IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            
            _factoryDelegates = new Dictionary<WindowType, Func<UniTask<IWindowPresenter>>>
            {
                [WindowType.Buildings] = () => CreateWindow<ChooseBuildingView, ChooseBuildingPresenter>(config.ChooseBuildingViewPrefab),
                [WindowType.EndGame] = () => CreateWindow<EndGameView, EndGamePresenter>(config.EndGameViewPrefab),
                [WindowType.ContinueByAd] = () => CreateWindow<ContinueByAdView, ContinueByAdPresenter>(config.ContinueByAdViewPrefab),
            };
        }

        private async UniTask<IWindowPresenter> CreateWindow<TView, TPresenter>(AssetReference prefabReference) where TView : MonoBehaviour
        {
            var prefab = await _assetProvider.Load<GameObject>(prefabReference);

            var view = _instantiator.InstantiatePrefabForComponent<TView>(prefab.GetComponent<TView>());

            var presenter = _instantiator.Instantiate<TPresenter>(new object[] { view });

            return (IWindowPresenter)presenter;
        }

        public async UniTask<IWindowPresenter> Create(WindowType type)
        {
            if (_factoryDelegates.TryGetValue(type, out var factory))
                return await factory();

            throw new ArgumentOutOfRangeException(nameof(type), type, $"No factory registered for window type {type}");
        }
    }
}