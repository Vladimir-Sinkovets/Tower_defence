using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class PlayerUnitFindTargetSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _playerUnits;
        
        private Stash<FollowTarget> _followTargetStash;
        private Filter _enemyUnits;
        private Stash<Position> _positionStash;

        public void OnAwake()
        {
            _playerUnits = World.Filter
                .With<PlayerUnit>()
                .Without<FollowTarget>()
                .Build();
            
            _enemyUnits = World.Filter
                .With<EnemyUnit>()
                .With<Position>()
                .Build();
                
            _followTargetStash = World.GetStash<FollowTarget>();
            _positionStash = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerUnit in _playerUnits)
            {
                ref var playerPosition = ref _positionStash.Get(playerUnit);

                Entity nearestEnemy = default;
                var nearestSqrDistance = float.MaxValue;
                var found = false;

                foreach (var enemyUnit in _enemyUnits)
                {
                    ref var enemyPosition = ref _positionStash.Get(enemyUnit);

                    var sqrDistance = (enemyPosition.Value - playerPosition.Value).sqrMagnitude;
                    
                    if (sqrDistance < nearestSqrDistance)
                    {
                        nearestSqrDistance = sqrDistance;
                        nearestEnemy = enemyUnit;
                        found = true;
                    }
                }

                if (found)
                {
                    _followTargetStash.Add(playerUnit, new FollowTarget
                    {
                        Target = nearestEnemy
                    });
                }
            }
        }
        
        public void Dispose() { }
    }
}   