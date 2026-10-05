using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class FollowTargetSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _units;
        
        private Stash<MoveDirection> _moveDirectionStash;
        private Stash<Target> _targetStash;
        private Stash<Position> _positionStash;
        private Stash<AttackRange> _attackRangeStash;

        public void OnAwake()
        {
            _units = World.Filter
                .With<Target>()
                .With<Position>()
                .With<AttackRange>()
                .Build();

            _moveDirectionStash = World.GetStash<MoveDirection>();
            _targetStash = World.GetStash<Target>();
            _positionStash = World.GetStash<Position>();
            _attackRangeStash = World.GetStash<AttackRange>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _units)
            {
                ref var target = ref _targetStash.Get(entity).Value;

                if (!World.Has(target))
                {
                    _targetStash.Remove(entity);
                    _moveDirectionStash.Set(entity, new() { Value = Vector3.zero });
                    
                    continue;
                }

                ref var unitPosition = ref _positionStash.Get(entity);
                ref var targetPosition = ref _positionStash.Get(target);
                ref var attackRange = ref _attackRangeStash.Get(entity);

                if ((targetPosition.Value - unitPosition.Value).magnitude < attackRange.Value)
                {
                    _moveDirectionStash.Set(entity, new() { Value = Vector3.zero });
                }
                else
                {
                    var moveDirection = (targetPosition.Value - unitPosition.Value).normalized;

                    _moveDirectionStash.Set(entity, new() { Value =  moveDirection });
                }
            }
        }
        
        public void Dispose() { }
    }
}