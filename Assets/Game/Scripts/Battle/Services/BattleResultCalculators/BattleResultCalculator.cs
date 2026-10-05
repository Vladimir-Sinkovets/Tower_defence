using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs.Spawn;
using Assets.Game.Scripts.Battle.Ecs.Statistics;
using Assets.Game.Scripts.Battle.Services.WorldAccessors;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Services.BattleResultCalculators
{
    public class BattleResultCalculator : IBattleResultCalculator
    {
        private readonly BattleResultConfig _config;
        private readonly World _world;

        public BattleResultCalculator(IWorldAccessor worldAccessor, BattleResultConfig config)
        {
            _config = config;
            _world = worldAccessor.World;
        }
        
        public BattleResult GetGameOverResult()
        {
            var wavesCount = _world
                .GetStash<EnemySpawner>().Get(
                    _world.Filter.With<EnemySpawner>().Build()
                        .First())
                .WaveCount;
            
            var killsCount = _world
                .GetStash<GameStatistic>().Get(
                    _world.Filter.With<GameStatistic>().Build()
                        .First())
                .Kills;
            
            return new()
            {
                EarnedMetaCurrency = killsCount * _config.CurrencyPerKill + wavesCount * _config.CurrencyPerWave,
                Kills = killsCount,
                Waves = wavesCount,
            };
        }
    }
}