using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public class GameResultSaver : IGameResultSaver
    {
        private readonly ISaveService _saveService;

        public GameResultSaver(ISaveService saveService) => _saveService = saveService;

        public void ApplySaveData(int earnedMetaCurrency, int wavesCount)
        {
            var data = _saveService.GetSaveData();

            data.MetaCurrency += earnedMetaCurrency;
            
            if (data.WavesRecord < wavesCount)
                data.WavesRecord = wavesCount;
            
            _saveService.Save(data);
        }
    }
}