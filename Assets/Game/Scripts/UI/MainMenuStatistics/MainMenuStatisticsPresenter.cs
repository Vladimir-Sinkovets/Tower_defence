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
            SetStatisticsView();

            _saveService.OnSaved += OnSavedHandler;
        }

        private void OnSavedHandler() => SetStatisticsView();

        private void SetStatisticsView()
        {
            var data = _saveService.GetSaveData();
            
            _view.SetMetaCurrency(data.MetaCurrency.ToString());
            _view.SetWavesRecord(data.WavesRecord.ToString());
        }

        public void Dispose() => _saveService.OnSaved -= OnSavedHandler;
    }
}