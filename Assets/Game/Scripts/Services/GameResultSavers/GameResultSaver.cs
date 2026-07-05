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

        public void ApplyMetaCurrency(int earnedMetaCurrency)
        {
            _saveService.MetaCurrency += earnedMetaCurrency;
            
            _saveService.Save();
        }

        public void ApplyWavesRecord(int wavesCount)
        {
            if (_saveService.WavesRecord < wavesCount)
                _saveService.WavesRecord = wavesCount;
            
            _saveService.Save();
        }
    }
}