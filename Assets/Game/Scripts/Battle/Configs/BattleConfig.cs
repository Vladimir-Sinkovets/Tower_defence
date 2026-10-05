using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    [CreateAssetMenu(fileName = "Battle_config", menuName = "Battle/Battle config")]
    public class BattleConfig : ScriptableObject
    {
        public int StartCurrency = 10;
    }
}