using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public class GameResultSaver : IGameResultSaver
    {
        private readonly ISaveService _saveService;

        public GameResultSaver(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void ApplySaveData(int earnedMetaCurrency, int wavesCount)
        {
            _saveService.MetaCurrency += earnedMetaCurrency;
            
            if (_saveService.WavesRecord < wavesCount)
                _saveService.WavesRecord = wavesCount;
            
            _saveService.Save();
        }
    }
}