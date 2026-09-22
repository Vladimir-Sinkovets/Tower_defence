using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature.Systems
{
    public class RemoveDeadSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _dead;

        public void OnAwake()
        {
            _dead = World.Filter
                .With<Dead>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _dead)
            {
                World.RemoveEntity(entity);
            }
        }

        public void Dispose() { }
    }
}