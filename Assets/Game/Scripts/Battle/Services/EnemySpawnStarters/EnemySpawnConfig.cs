using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.EnemySpawnStarters
{
    [CreateAssetMenu(fileName = "Enemy_spawn_config", menuName = "Battle/Enemy spawn config")]
    public class EnemySpawnConfig : ScriptableObject
    {
        public float TimeBetweenSpawn;
        public GameObject EnemyPrefab;
    }
}