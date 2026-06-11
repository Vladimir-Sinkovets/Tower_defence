using System;
using Assets.Game.Scripts.Saves;
using Zenject;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsPresenter : IInitializable, IDisposable
    {
        private readonly IMainMenuStatisticsView _view;
        private readonly SaveData _saveData;

        public MainMenuStatisticsPresenter(IMainMenuStatisticsView view, SaveData saveData)
        {
            _view = view;
            _saveData = saveData;
        }

        public void Initialize()
        {
            SetStatisticsView();

            _saveData.MetaCurrencyChanged += OnMetaCurrencyChangedHandler;
        }

        private void OnMetaCurrencyChangedHandler() => SetStatisticsView();

        private void SetStatisticsView()
        {
            _view.SetMetaCurrency(_saveData.MetaCurrency.ToString());
            _view.SetWavesRecord(_saveData.WavesRecord.ToString());
        }

        public void Dispose() => _saveData.MetaCurrencyChanged -= OnMetaCurrencyChangedHandler;
    }
}