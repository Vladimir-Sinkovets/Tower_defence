using Assets.Game.Scripts.Services.Configs;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public class RewardCalculator : IRewardCalculator
    {
        private readonly IGameSettingsAccessor _gameSettingsAccessor;

        public RewardCalculator(IGameSettingsAccessor gameSettingsAccessor) => _gameSettingsAccessor = gameSettingsAccessor;

        public int CalculateMetaCurrency(int wavesCount, int killedEnemiesCount)
        {
            var metaCurrencySettings = _gameSettingsAccessor.Settings.MetaCurrencySettings;
            
            return wavesCount * metaCurrencySettings.MetaCurrencyPerWave +
                   killedEnemiesCount * metaCurrencySettings.MetaCurrencyPerKill;
        }
    }
}