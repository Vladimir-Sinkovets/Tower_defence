using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Input.Systems
{
    public class InputCleanUpSystem : ICleanupSystem
    {
        public World World { get; set; }
        
        private Filter _clickEventsFilter;
        private Filter _clickOnFieldEventsFilter;

        public void OnAwake()
        {
            _clickEventsFilter = World.Filter
                .With<ClickEvent>()
                .Build();
            
            _clickOnFieldEventsFilter = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var clickEventEntity in _clickEventsFilter)
            {
                World.RemoveEntity(clickEventEntity);
            }

            foreach (var clickOnFieldEventEntity in _clickOnFieldEventsFilter)
            {
                World.RemoveEntity(clickOnFieldEventEntity);
            }
        }

        public void Dispose() { }
    }
}