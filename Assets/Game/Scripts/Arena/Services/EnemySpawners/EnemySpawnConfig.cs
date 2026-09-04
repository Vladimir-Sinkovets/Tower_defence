using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemySpawners
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Arena/Enemy spawn config")]
    public class EnemySpawnConfig : ScriptableObject
    {
        public ArenaEnemyConfig EnemyConfig;
        public float TimeBetweenSpawns = 2f;
    }
}