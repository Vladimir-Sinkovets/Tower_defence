using Assets.Game.Scripts.Services.Registries;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyAccessors
{
    public class EnemyAccessor : IEnemyAccessor
    {
        private readonly Registry<ArenaEnemy> _enemyRegistry;

        public EnemyAccessor(Registry<ArenaEnemy> enemyRegistry)
        {
            _enemyRegistry = enemyRegistry;
        }
        
        public ArenaEnemy FindNearestEnemy(Vector3 position, float radius)
        {
            if (_enemyRegistry.All == null)
                return null;

            var minDistance = float.MaxValue;
            ArenaEnemy nearestEnemy = null;

            foreach (var enemy in _enemyRegistry.All)
            {
                if (enemy.Health.IsDead)
                    continue;

                var distance = Vector3.Distance(enemy.transform.position, position);

                if (distance <= radius && minDistance > distance)
                {
                    minDistance = distance;

                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }

    }
}