using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    [CreateAssetMenu(fileName = "Player_units_config", menuName = "Battle/Player units config")]
    public class PlayerUnitsConfig : ScriptableObject
    {
        public GameObject Prefab;
    }
}