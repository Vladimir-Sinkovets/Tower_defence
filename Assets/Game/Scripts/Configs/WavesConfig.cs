using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Waves_config", menuName = "Configs/Wave config")]
    public class WavesConfig : ScriptableObject
    {
        public EnemyConfig EnemyConfig;
    }
}