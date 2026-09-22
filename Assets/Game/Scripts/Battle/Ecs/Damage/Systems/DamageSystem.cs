using Assets.Game.Scripts.Battle.Ecs.HealthFeature;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Damage.Systems
{
    public class DamageSystem : ISystem
    {
        public World World { get; set; }

        private Filter _damageEvents;
        
        private Stash<DamageEvent> _damageStash;
        private Stash<Health> _healthStash;

        public void OnAwake()
        {
            _damageEvents = World.Filter
                .With<DamageEvent>()
                .Build();
            
            _damageStash = World.GetStash<DamageEvent>();
            _healthStash = World.GetStash<Health>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _damageEvents)
            {
                ref var damage = ref _damageStash.Get(entity);

                if (_healthStash.Has(damage.Target))
                {
                    ref var health = ref _healthStash.Get(damage.Target);

                    health.Hp -= damage.Damage;
                }
                
                World.RemoveEntity(entity);
            }
        }

        public void Dispose() { }
    }
}