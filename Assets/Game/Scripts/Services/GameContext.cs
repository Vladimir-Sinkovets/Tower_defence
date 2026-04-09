using Assets.Game.Scripts.Enemies;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services
{
    public class GameContext
    {
        private HashSet<Damageable> _enemies = new();

        public IEnumerable<Damageable> All => _enemies;
        public bool RegisterEnemy(Damageable enemy) => _enemies.Add(enemy);
        public bool UnregisterEnemy(Damageable enemy) => _enemies.Remove(enemy);
    }
}