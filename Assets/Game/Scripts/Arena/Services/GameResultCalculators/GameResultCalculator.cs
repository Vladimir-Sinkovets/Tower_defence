using Assets.Game.Scripts.Arena.Services.GameStatistic;

namespace Assets.Game.Scripts.Arena.Services.GameResultCalculators
{
    public class GameResultCalculator : IGameResultCalculator
    {
        private readonly IGameStatistics _gameStatistics;
        private readonly ArenaConfig _config;

        public GameOverResult GameOverResult { get; private set; }
        
        public GameResultCalculator(IGameStatistics gameStatistics, ArenaConfig config)
        {
            _gameStatistics = gameStatistics;
            _config = config;
        }
        
        public GameOverResult Calculate()
        {
            var earnedMetaCurrency = CalculateMetaCurrency(_gameStatistics.KilledEnemiesCount);

            GameOverResult = new GameOverResult()
            {
                Kills = _gameStatistics.KilledEnemiesCount,
                EarnedMetaCurrency = earnedMetaCurrency,
            };
            
            return GameOverResult;
        }

        private int CalculateMetaCurrency(int killedEnemiesCount) => killedEnemiesCount * _config.MetacurrencyPerKill;
    }

    public class GameOverResult
    {
        public int Kills;
        public int EarnedMetaCurrency;
    }
}