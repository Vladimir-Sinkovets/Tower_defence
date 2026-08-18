using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public class GameResultSaver : IGameResultSaver
    {
        private readonly SaveData _saveData;
        private readonly ISaveService _saveService;

        public GameResultSaver(ISaveService saveService)
        {
            _saveService = saveService;
            _saveData = saveService.SaveData;
        }

        public void ApplyMetaCurrency(int earnedMetaCurrency)
        {
            _saveData.MetaCurrency += earnedMetaCurrency;
            
            _saveService.Save();
        }

        public void ApplyWavesRecord(int wavesCount)
        {
            if (_saveData.WavesRecord < wavesCount)
                _saveData.WavesRecord = wavesCount;
            
            _saveService.Save();
        }
    }
}