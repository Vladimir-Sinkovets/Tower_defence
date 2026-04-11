using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Meta_currency_config", menuName = "Configs/Meta currency config")]
    public class MetaCurrencyConfig : ScriptableObject
    {
        public int MetaCurrencyPerWave = 5;
        public int MetaCurrencyPerKill = 1;
    }
}