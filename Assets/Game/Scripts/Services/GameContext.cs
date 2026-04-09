using Assets.Game.Scripts.Enemies;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services
{
    public class GameContext
    {
        private HashSet<Enemy> _enemies = new();

        public IEnumerable<Enemy> AllEnemies => _enemies;
        public bool RegisterEnemy(Enemy enemy) => _enemies.Add(enemy);
        public bool UnregisterEnemy(Enemy enemy) => _enemies.Remove(enemy);
    }
}