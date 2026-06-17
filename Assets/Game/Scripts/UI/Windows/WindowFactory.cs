using System;
using Assets.Game.Scripts.Services.AssetProviders;
using Assets.Game.Scripts.UI.Windows.Buildings;
using Assets.Game.Scripts.UI.Windows.EndGame;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly WindowViewsConfig _config;
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public WindowFactory(WindowViewsConfig config, IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _config = config;
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<IWindowPresenter> Create(WindowType type)
        {
            switch (type)
            {
                case WindowType.Buildings:

                    var chooseBuildingViewPrefab = await _assetProvider.Load<GameObject>(_config.ChooseBuildingViewPrefab);
                    
                    var chooseBuildingView = _instantiator.InstantiatePrefabForComponent<ChooseBuildingView>(chooseBuildingViewPrefab.GetComponent<ChooseBuildingView>());

                    var chooseBuildingPresenter = _instantiator.Instantiate<ChooseBuildingPresenter>(new[] { chooseBuildingView });

                    return chooseBuildingPresenter;
                
                case WindowType.EndGame:
                    
                    var endGameViewPrefab = await _assetProvider.Load<GameObject>(_config.EndGameViewPrefab);
                    
                    var endGameView = _instantiator.InstantiatePrefabForComponent<EndGameView>(endGameViewPrefab.GetComponent<EndGameView>());

                    var endGamePresenter = _instantiator.Instantiate<EndGamePresenter>(new[] { endGameView });

                    return endGamePresenter;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}