using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class EnemyAccessor : IEnemyAccessor
    {
        private readonly Registry<Enemy> _enemyRegistry;

        public EnemyAccessor(Registry<Enemy> enemyRegistry)
        {
            _enemyRegistry = enemyRegistry;
        }
        
        public Enemy FindNearestEnemy(Vector3 position, float radius)
        {
            if (_enemyRegistry.All == null)
                return null;

            var minDistance = float.MaxValue;
            Enemy nearestEnemy = null;

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

    public interface IEnemyAccessor
    {
        Enemy FindNearestEnemy(Vector3 position, float radius);
    }
}