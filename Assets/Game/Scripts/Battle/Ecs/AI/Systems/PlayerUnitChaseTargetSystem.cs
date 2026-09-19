using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class PlayerUnitChaseTargetSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _playerUnits;
        
        private Stash<MoveDirection> _movementStash;
        private Stash<FollowTarget> _followTargetStash;
        private Stash<Position> _positionStash;

        public void OnAwake()
        {
            _playerUnits = World.Filter
                .With<PlayerUnit>()
                .With<FollowTarget>()
                .Build();

            _movementStash = World.GetStash<MoveDirection>();
            _followTargetStash = World.GetStash<FollowTarget>();
            _positionStash = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerUnit in _playerUnits)
            {
                ref var target = ref _followTargetStash.Get(playerUnit).Target;

                if (World.IsDisposed(target))
                    continue;

                ref var unitPosition = ref _positionStash.Get(playerUnit);
                ref var targetPosition = ref _positionStash.Get(target);

                var moveDirection = (targetPosition.Value - unitPosition.Value).normalized;

                _movementStash.Set(playerUnit, new() { Value =  moveDirection });
            }
        }
        
        public void Dispose() { }
    }
}