using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Input.Systems
{
    public class InputCleanUpSystem : ICleanupSystem
    {
        public World World { get; set; }
        
        private Filter _clickEventsFilter;
        private Filter _clickOnFieldEventsFilter;
        
        private Stash<ClickEvent> _clickEventStash;
        private Stash<ClickEvent> _clickOnFieldEventStash;

        public void OnAwake()
        {
            _clickEventsFilter = World.Filter
                .With<ClickEvent>()
                .Build();
            
            _clickOnFieldEventsFilter = World.Filter
                .With<ClickOnFieldEvent>()
                .Build();

            _clickEventStash = World.GetStash<ClickEvent>();
            _clickOnFieldEventStash = World.GetStash<ClickEvent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var clickEventEntity in _clickEventsFilter)
            {
                _clickEventStash.Remove(clickEventEntity);
            }

            foreach (var clickOnFieldEventEntity in _clickOnFieldEventsFilter)
            {
                _clickOnFieldEventStash.Remove(clickOnFieldEventEntity);
            }
        }

        public void Dispose() { }
    }
}