using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature.Systems
{
    public class DeathSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _alives;
        
        private Stash<Health> _healthStash;
        private Stash<Dead> _deadStash;
        private Stash<DeathEvent> _deathEventStash;

        public void OnAwake()
        {
            _alives = World.Filter
                .With<Health>()
                .Without<Dead>()
                .Build();
            
            _healthStash = World.GetStash<Health>();
            _deadStash = World.GetStash<Dead>();
            _deathEventStash = World.GetStash<DeathEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _alives)
            {
                ref var health = ref _healthStash.Get(entity);
                
                if (health.Hp <= 0)
                {
                    _deadStash.Add(entity);
                    
                    var eventEntity = World.CreateEntity();

                    _deathEventStash.Set(eventEntity, new() { Target = entity });
                }
            }
        }

        public void Dispose() { }
    }
}