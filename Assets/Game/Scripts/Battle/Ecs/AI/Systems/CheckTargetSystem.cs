using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class CheckTargetSystem : ISystem
    {
        private Filter _followers;
        
        private Stash<FollowTarget> _followTargetStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _followers = World.Filter
                .With<FollowTarget>()
                .Build();

            _followTargetStash = World.GetStash<FollowTarget>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _followers)
            {
                ref var followTarget = ref _followTargetStash.Get(entity);

                if (World.IsDisposed(followTarget.Target))
                {
                    _followTargetStash.Remove(entity);
                }
            }
        }
        
        public void Dispose() { }
    }
}