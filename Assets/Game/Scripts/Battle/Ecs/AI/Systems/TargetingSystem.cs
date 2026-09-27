using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class TargetingSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _units;
        private Filter _targets;
        
        private Stash<Target> _followTargetStash;
        private Stash<Position> _positionStash;
        private Stash<Team> _targetTeamStash;

        public void OnAwake()
        {
            _units = World.Filter
                .With<Team>()
                .With<Position>()
                .Without<Target>()
                .Build();
                
            _targets = World.Filter
                .With<Team>()
                .With<Position>()
                .Build();
            
            _followTargetStash = World.GetStash<Target>();
            _positionStash = World.GetStash<Position>();
            _targetTeamStash = World.GetStash<Team>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var unit in _units)
            {
                ref var unitPosition = ref _positionStash.Get(unit);
                ref var unitTeam = ref _targetTeamStash.Get(unit);

                Entity nearestTarget = default;
                var nearestSqrDistance = float.MaxValue;
                var found = false;

                foreach (var targetUnit in _targets)
                {
                    ref var targetTeam = ref _targetTeamStash.Get(targetUnit);

                    if (unitTeam.Index == targetTeam.Index)
                        continue;
                    
                    ref var targetPosition = ref _positionStash.Get(targetUnit);
                    
                    var sqrDistance = (targetPosition.Value - unitPosition.Value).sqrMagnitude;
                    
                    if (sqrDistance < nearestSqrDistance)
                    {
                        nearestSqrDistance = sqrDistance;
                        nearestTarget = targetUnit;
                        found = true;
                    }
                }

                if (found)
                {
                    _followTargetStash.Add(unit, new Target
                    {
                        Value = nearestTarget
                    });
                }
            }
        }
        
        public void Dispose() { }
    }
}   