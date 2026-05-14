using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsPresenter
    {
        public MainMenuStatisticsPresenter(IMainMenuStatisticsView view, ISaveService saveService)
        {
            var data = saveService.GetSaveData();
            
            view.SetMetaCurrency(data.MetaCurrency.ToString());
            view.SetWavesRecord(data.WavesRecord.ToString());
        }
    }
}