using System;
using Assets.Game.Scripts.UI.Windows.Buildings;
using Assets.Game.Scripts.UI.Windows.EndGame;
using Zenject;

namespace Assets.Game.Scripts.UI.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly WindowViewsConfig _config;
        private readonly IInstantiator _instantiator;

        public WindowFactory(WindowViewsConfig config, IInstantiator instantiator)
        {
            _config = config;
            _instantiator = instantiator;
        }
        
        public IWindowPresenter Create(WindowType type)
        {
            switch (type)
            {
                case WindowType.Buildings:
                    
                    var chooseBuildingView = _instantiator.InstantiatePrefabForComponent<ChooseBuildingView>(_config.ChooseBuildingViewPrefab);

                    var chooseBuildingPresenter = _instantiator.Instantiate<ChooseBuildingPresenter>(new[] { chooseBuildingView });

                    return chooseBuildingPresenter;
                
                case WindowType.EndGame:
                    
                    var endGameView = _instantiator.InstantiatePrefabForComponent<EndGameView>(_config.EndGameViewPrefab);

                    var endGamePresenter = _instantiator.Instantiate<EndGamePresenter>(new[] { endGameView });

                    return endGamePresenter;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public interface IWindowFactory
    {
        IWindowPresenter Create(WindowType type);
    }
}