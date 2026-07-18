using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.RewardCalculators;
using Assets.Game.Scripts.Services.Statistics;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameResults
{
    public class GameResultCalculator : IGameResultCalculator
    {
        private readonly GameStatistics _gameStatistics;
        private readonly CurrencyBank _currencyBank;
        private readonly IRewardCalculator _rewardCalculator;
        private readonly IWavesController _wavesController;

        public GameOverResult GameOverResult { get; private set; }
        
        public GameResultCalculator(
            IWavesController wavesController,
            IRewardCalculator rewardCalculator,
            CurrencyBank currencyBank,
            GameStatistics gameStatistics)
        {
            _wavesController = wavesController;
            _rewardCalculator = rewardCalculator;
            _currencyBank = currencyBank;
            _gameStatistics = gameStatistics;
        }
        
        public async UniTask<GameOverResult> CalculateAsync()
        {
            var earnedMetaCurrency = await _rewardCalculator.CalculateMetaCurrencyAsync(_wavesController.WavesCount, _gameStatistics.KilledEnemiesCount);

            GameOverResult = new GameOverResult()
            {
                Waves = _wavesController.WavesCount,
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