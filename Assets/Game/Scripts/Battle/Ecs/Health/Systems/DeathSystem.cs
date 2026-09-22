using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature.Systems
{
    public class DeathSystem : ISystem
    {
        private Filter _alive;
        private Stash<Health> _healthStash;
        private Stash<Dead> _deadStash;
        public World World { get; set; }

        public void OnAwake()
        {
            _alive = World.Filter
                .With<Health>()
                .Without<Dead>()
                .Build();
            
            _healthStash = World.GetStash<Health>();
            _deadStash = World.GetStash<Dead>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _alive)
            {
                ref var health = ref _healthStash.Get(entity);
                
                if (health.Hp <= 0)
                {
                    _deadStash.Add(entity);
                }
            }
        }

        public void Dispose() { }
    }
}