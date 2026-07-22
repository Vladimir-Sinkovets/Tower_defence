using System;
using Assets.Game.Scripts.Saves;
using Zenject;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsPresenter : IInitializable, IDisposable
    {
        private readonly IMainMenuStatisticsView _view;
        private readonly ISaveService _saveService;

        public MainMenuStatisticsPresenter(IMainMenuStatisticsView view, ISaveService saveService)
        {
            _view = view;
            _saveService = saveService;
        }

        public void Initialize()
        {
            _view.SetMetaCurrency(_saveService.MetaCurrency.ToString());
            
            _view.SetWavesRecord(_saveService.WavesRecord.ToString());

            _saveService.OnMetaCurrencyChanged += OnOnMetaCurrencyChangedHandler;
        }

        private void OnOnMetaCurrencyChangedHandler() => _view.SetMetaCurrency(_saveService.MetaCurrency.ToString());

        public void Dispose() => _saveService.OnMetaCurrencyChanged -= OnOnMetaCurrencyChangedHandler;
    }
}