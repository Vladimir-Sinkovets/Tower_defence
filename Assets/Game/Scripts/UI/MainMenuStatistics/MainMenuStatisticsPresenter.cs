using System;
using Assets.Game.Scripts.Saves;
using Zenject;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsPresenter : IInitializable, IDisposable
    {
        private readonly IMainMenuStatisticsView _view;
        private readonly GameDataHolder _gameDataHolder;

        public MainMenuStatisticsPresenter(IMainMenuStatisticsView view, GameDataHolder gameDataHolder)
        {
            _view = view;
            _gameDataHolder = gameDataHolder;
        }

        public void Initialize()
        {
            _view.SetMetaCurrency(_gameDataHolder.Data.MetaCurrency.ToString());
            
            _view.SetWavesRecord(_gameDataHolder.Data.WavesRecord.ToString());

            _gameDataHolder.Data.OnChanged += OnChangedHandler;
        }

        private void OnChangedHandler() => _view.SetMetaCurrency(_gameDataHolder.Data.MetaCurrency.ToString());

        public void Dispose() => _gameDataHolder.Data.OnChanged -= OnChangedHandler;
    }
}