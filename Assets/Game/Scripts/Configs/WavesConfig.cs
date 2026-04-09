using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Waves_config", menuName = "Configs/Wave config")]
    public class WavesConfig : ScriptableObject
    {
        public int BaseEnemyCount = 1;
        public int NewEnemiesPerWave = 1;
        public int IntervalBetweenWaves = 3;
    }
}