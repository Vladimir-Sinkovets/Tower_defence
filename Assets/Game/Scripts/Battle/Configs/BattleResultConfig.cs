using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    [CreateAssetMenu(fileName = "Battle_result_config", menuName = "Battle/Battle result config")]
    public class BattleResultConfig : ScriptableObject
    {
        public int CurrencyPerWave = 2;
        public int CurrencyPerKill = 1;
    }
}