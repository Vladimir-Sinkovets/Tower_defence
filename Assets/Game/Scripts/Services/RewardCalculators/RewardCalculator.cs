using Assets.Game.Scripts.Services.Configs;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public class RewardCalculator : IRewardCalculator
    {
        private readonly GameSettings _gameSettings;

        public RewardCalculator(GameSettingsService gameSettingsService) => _gameSettings = gameSettingsService.GameSettings;

        public int CalculateMetaCurrency(int wavesCount, int killedEnemiesCount) =>
            wavesCount * _gameSettings.MetaCurrencySettings.MetaCurrencyPerWave +
            killedEnemiesCount * _gameSettings.MetaCurrencySettings.MetaCurrencyPerKill;
    }
}