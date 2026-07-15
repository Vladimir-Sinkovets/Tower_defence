using System;
using Assets.Game.Scripts.Services.Configs.Enemies;

namespace Assets.Game.Scripts.Services.Configs.Waves
{
    [Serializable]
    public class WavesSettings
    {
        public EnemySettings EnemySettings;
        public int BaseEnemyCount = 1;
        public int NewEnemiesPerWave = 1;
        public float IntervalBetweenWaves = 3.0f;
        public float IntervalBetweenEnemies = 1.0f;
    }
}