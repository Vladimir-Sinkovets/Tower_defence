using Assets.Game.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public class RewardCalculator : IRewardCalculator
    {
        private readonly GameSettingsService _gameSettingsService;

        public RewardCalculator(GameSettingsService gameSettingsService) => _gameSettingsService = gameSettingsService;

        public async UniTask<int> CalculateMetaCurrencyAsync(int wavesCount, int killedEnemiesCount)
        {
            var gameSettings = await _gameSettingsService.GetSettingsAsync();
            
            return wavesCount * gameSettings.MetaCurrencySettings.MetaCurrencyPerWave +
                   killedEnemiesCount * gameSettings.MetaCurrencySettings.MetaCurrencyPerKill;
        }
    }
}