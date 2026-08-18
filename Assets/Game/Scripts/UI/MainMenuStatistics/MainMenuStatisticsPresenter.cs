using System;
using Assets.Game.Scripts.Saves;
using Zenject;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsPresenter : IInitializable, IDisposable
    {
        private readonly IMainMenuStatisticsView _view;
        private readonly SaveData _saveData;

        public MainMenuStatisticsPresenter(IMainMenuStatisticsView view, ISaveService saveService)
        {
            _view = view;
            _saveData = saveService.SaveData;
        }

        public void Initialize()
        {
            _view.SetMetaCurrency(_saveData.MetaCurrency.ToString());
            
            _view.SetWavesRecord(_saveData.WavesRecord.ToString());

            _saveData.OnChanged += OnChangedHandler;
        }

        private void OnChangedHandler() => _view.SetMetaCurrency(_saveData.MetaCurrency.ToString());

        public void Dispose() => _saveData.OnChanged -= OnChangedHandler;
    }
}