using Assets.Game.Scripts.Enemies.Factories;
using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Waves_config", menuName = "Configs/Wave config")]
    public class WavesConfig : ScriptableObject
    {
        public EnemyFactory EnemyFactory;
        public int BaseEnemyCount = 1;
        public int NewEnemiesPerWave = 1;
        public float IntervalBetweenWaves = 3.0f;
        public float IntervalBetweenEnemies = 1.0f;
    }
}