using Assets.Game.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public class RewardCalculator : IRewardCalculator
    {
        private readonly GameSettingsService _gameSettingsService;

        public RewardCalculator(GameSettingsService gameSettingsService) => _gameSettingsService = gameSettingsService;

        public int CalculateMetaCurrency(int wavesCount, int killedEnemiesCount)
        {
            var metaCurrencySettings = _gameSettingsService.Settings.MetaCurrencySettings;
            
            return wavesCount * metaCurrencySettings.MetaCurrencyPerWave +
                   killedEnemiesCount * metaCurrencySettings.MetaCurrencyPerKill;
        }
    }
}