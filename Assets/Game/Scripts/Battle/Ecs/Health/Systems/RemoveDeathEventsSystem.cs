using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature.Systems
{
    public class RemoveDeathEventsSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _deathEvents;

        public void OnAwake()
        {
            _deathEvents = World.Filter
                .With<DeathEvent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _deathEvents)
            {
                World.RemoveEntity(entity);
            }
        }

        public void Dispose() { }
    }
}