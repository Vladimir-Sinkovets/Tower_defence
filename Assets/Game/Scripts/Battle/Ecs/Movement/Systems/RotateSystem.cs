using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Movement.Systems
{
    public class RotateSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _entities;
        
        private Stash<Rotation> _rotationStash;
        private Stash<MoveDirection> _moveDirectionStash;

        public void OnAwake()
        {
            _entities = World.Filter
                .With<Rotation>()
                .With<MoveDirection>()
                .Build();

            _rotationStash = World.GetStash<Rotation>();
            _moveDirectionStash = World.GetStash<MoveDirection>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref var rotation = ref _rotationStash.Get(entity);
                ref var moveDirection = ref _moveDirectionStash.Get(entity);

                Vector3 direction = moveDirection.Value;

                if (direction.sqrMagnitude < 0.001f)
                    continue;

                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                rotation.Value = Quaternion.RotateTowards(
                    rotation.Value,
                    targetRotation,
                    360f * deltaTime
                );
            }
        }
        
        public void Dispose() { }
    }
}