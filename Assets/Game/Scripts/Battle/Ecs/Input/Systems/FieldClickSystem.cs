using Assets.Game.Scripts.Battle.Services.Raycasts;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Input.Systems
{
    public class FieldClickSystem : ISystem
    {
        public World World { get; set; }

        private readonly IPlaneRaycastService _planeRaycastService;
        
        private Filter _clickFilter;
        
        private Stash<ClickEvent> _clickStash;
        private Stash<ClickOnFieldEvent> _fieldClickStash;

        public FieldClickSystem(IPlaneRaycastService planeRaycastService) => _planeRaycastService = planeRaycastService;

        public void OnAwake()
        {
            _clickFilter = World.Filter
                .With<ClickEvent>()
                .Build();
            
            _clickStash = World.GetStash<ClickEvent>();
            _fieldClickStash = World.GetStash<ClickOnFieldEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var clickEventEntity in _clickFilter)
            {
                ref var clickEvent = ref _clickStash.Get(clickEventEntity);

                if (_planeRaycastService.TryRaycast(clickEvent.ScreenPosition, out var point))
                {
                    var entity = World.CreateEntity();
                    
                    _fieldClickStash.Set(entity, new ClickOnFieldEvent()
                    {
                        Position = point,
                    });
                }
            }
        }
        
        public void Dispose() { }
    }
}