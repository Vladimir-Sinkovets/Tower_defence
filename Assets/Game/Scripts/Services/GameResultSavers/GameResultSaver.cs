using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public class GameResultSaver : IGameResultSaver
    {
        private readonly SaveData _saveData;
        private readonly ISaveService _saveService;

        public GameResultSaver(SaveData saveData, ISaveService saveService)
        {
            _saveData = saveData;
            _saveService = saveService;
        }

        public void ApplySaveData(int earnedMetaCurrency, int wavesCount)
        {
            _saveData.MetaCurrency += earnedMetaCurrency;
            
            if (_saveData.WavesRecord < wavesCount)
                _saveData.WavesRecord = wavesCount;
            
            _saveService.Save();
        }
    }
}