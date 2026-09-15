using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyFactories
{
    public interface IEnemyFactory
    {
        ArenaEnemy Spawn(ArenaEnemyConfig enemyConfig, Vector3 position);
    }
}