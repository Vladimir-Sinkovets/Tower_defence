using Assets.Game.Scripts.Configs;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public class RewardCalculator : IRewardCalculator
    {
        private readonly MetaCurrencyConfig _metaCurrencyConfig;

        public RewardCalculator(MetaCurrencyConfig metaCurrencyConfig) => _metaCurrencyConfig = metaCurrencyConfig;

        public int CalculateMetaCurrency(int wavesCount, int killedEnemiesCount) =>
            wavesCount * _metaCurrencyConfig.MetaCurrencyPerWave +
            killedEnemiesCount * _metaCurrencyConfig.MetaCurrencyPerKill;
    }
}