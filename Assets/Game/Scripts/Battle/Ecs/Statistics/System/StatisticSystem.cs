using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Statistics.System
{
    public class StatisticSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _deathEvents;
        
        private Stash<DeathEvent> _deathEventStash;
        private Stash<Team> _teamStash;
        private Stash<GameStatistic> _gameStatisticStash;
        private Entity _gameStatisticEntity;

        public void OnAwake()
        {
            _deathEvents = World.Filter
                .With<DeathEvent>()
                .Build();

            _deathEventStash = World.GetStash<DeathEvent>();
            _gameStatisticStash = World.GetStash<GameStatistic>();
            _teamStash = World.GetStash<Team>();
            
            _gameStatisticEntity = World.CreateEntity();
            _gameStatisticStash.Set(_gameStatisticEntity);
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _deathEvents)
            {
                ref var deathEvent = ref _deathEventStash.Get(entity);
                ref var team = ref _teamStash.Get(deathEvent.Target);
                
                if (team.Index == TeamIndexes.Enemy)
                {
                    ref var statistic = ref _gameStatisticStash.Get(_gameStatisticEntity);

                    statistic.Kills++;
                }
            }
        }

        public void Dispose() { }
    }
}