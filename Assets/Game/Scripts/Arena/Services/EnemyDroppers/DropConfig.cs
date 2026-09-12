using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    [CreateAssetMenu(fileName = "Drop_config", menuName = "Arena/Drop config")]
    public class DropConfig : ScriptableObject
    {
        public string ExpPrefabName;
        public string CurrencyPrefabName;
        public int Experience = 2;
        public int Hp = 1;
    }
}