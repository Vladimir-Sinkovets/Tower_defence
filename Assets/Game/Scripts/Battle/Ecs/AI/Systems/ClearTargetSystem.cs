using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class ClearTargetSystem : ISystem
    {
        private Filter _followers;
        
        private Stash<FollowTarget> _followTargetStash;
        private Stash<MoveDirection> _moveDirectionStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _followers = World.Filter
                .With<FollowTarget>()
                .With<MoveDirection>()
                .Build();

            _followTargetStash = World.GetStash<FollowTarget>();
            _moveDirectionStash = World.GetStash<MoveDirection>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _followers)
            {
                ref var followTarget = ref _followTargetStash.Get(entity);

                if (World.IsDisposed(followTarget.Target))
                {
                    _followTargetStash.Remove(entity);
                    _moveDirectionStash.Set(entity, new() { Value = Vector3.zero });
                }
            }
        }
        
        public void Dispose() { }
    }
}