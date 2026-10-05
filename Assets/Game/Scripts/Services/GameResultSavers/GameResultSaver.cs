using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public class GameResultSaver : IGameResultSaver
    {
        private readonly GameDataHolder _gameDataHolder;
        private readonly ISaveService _saveService;

        public GameResultSaver(GameDataHolder gameDataHolder, ISaveService saveService)
        {
            _gameDataHolder = gameDataHolder;
            _saveService = saveService;
        }

        public void ApplyMetaCurrency(int earnedMetaCurrency)
        {
            _gameDataHolder.Data.MetaCurrency += earnedMetaCurrency;
            
            _saveService.Save();
        }

        public void ApplyWavesRecord(int wavesCount)
        {
            if (_gameDataHolder.Data.WavesRecord < wavesCount)
                _gameDataHolder.Data.WavesRecord = wavesCount;
            
            _saveService.Save();
        }
    }
}