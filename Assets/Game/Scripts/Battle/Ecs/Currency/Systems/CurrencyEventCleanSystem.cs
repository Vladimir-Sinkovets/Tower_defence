using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Systems
{
    public class CurrencyEventCleanSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _events;

        public void OnAwake()
        {
            _events = World.Filter
                .With<CurrencyChangedEvent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _events)
            {
                World.RemoveEntity(entity);
            }
        }

        public void Dispose() { }
    }
}