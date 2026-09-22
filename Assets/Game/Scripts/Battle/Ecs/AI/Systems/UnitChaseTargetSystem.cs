using Assets.Game.Scripts.Battle.Ecs.Damage;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class UnitChaseTargetSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _units;
        
        private Stash<MoveDirection> _movementStash;
        private Stash<FollowTarget> _followTargetStash;
        private Stash<Position> _positionStash;
        private Stash<AttackRequest> _attackStash;

        public void OnAwake()
        {
            _units = World.Filter
                .With<FollowTarget>()
                .Without<AttackRequest>()
                .Build();

            _movementStash = World.GetStash<MoveDirection>();
            _followTargetStash = World.GetStash<FollowTarget>();
            _positionStash = World.GetStash<Position>();
            _attackStash = World.GetStash<AttackRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var unit in _units)
            {
                ref var target = ref _followTargetStash.Get(unit).Target;

                if (World.IsDisposed(target))
                    continue;

                ref var unitPosition = ref _positionStash.Get(unit);
                ref var targetPosition = ref _positionStash.Get(target);

                if ((targetPosition.Value - unitPosition.Value).magnitude < 0.3f)
                {
                    _movementStash.Remove(unit);
                    
                    _attackStash.Set(unit, new());
                }
                else
                {
                    var moveDirection = (targetPosition.Value - unitPosition.Value).normalized;

                    _movementStash.Set(unit, new() { Value =  moveDirection });
                }
            }
        }
        
        public void Dispose() { }
    }
}