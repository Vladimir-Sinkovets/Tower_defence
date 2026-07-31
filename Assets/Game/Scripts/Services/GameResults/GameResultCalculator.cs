using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.RewardCalculators;
using Assets.Game.Scripts.Services.Statistics;

namespace Assets.Game.Scripts.Services.GameResults
{
    public class GameResultCalculator : IGameResultCalculator
    {
        private readonly IGameStatistics _gameStatistics;
        private readonly ICurrencyBank _currencyBank;
        private readonly IRewardCalculator _rewardCalculator;
        private readonly IWavesController _wavesController;

        public GameOverResult GameOverResult { get; private set; }
        
        public GameResultCalculator(
            IWavesController wavesController,
            IRewardCalculator rewardCalculator,
            ICurrencyBank currencyBank,
            IGameStatistics gameStatistics)
        {
            _wavesController = wavesController;
            _rewardCalculator = rewardCalculator;
            _currencyBank = currencyBank;
            _gameStatistics = gameStatistics;
        }
        
        public GameOverResult Calculate()
        {
            var earnedMetaCurrency = _rewardCalculator.CalculateMetaCurrency(_wavesController.WavesNumber, _gameStatistics.KilledEnemiesCount);

            GameOverResult = new GameOverResult()
            {
                Waves = _wavesController.WavesNumber,
                Kills = _gameStatistics.KilledEnemiesCount,
                Currency = _currencyBank.Total,
                EarnedMetaCurrency = earnedMetaCurrency,
            };
            
            return GameOverResult;
        }
    }

    public class GameOverResult
    {
        public int Waves;
        public int Kills;
        public int Currency;
        public int EarnedMetaCurrency;
    }
}