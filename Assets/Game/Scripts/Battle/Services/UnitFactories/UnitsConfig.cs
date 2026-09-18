using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    [CreateAssetMenu(fileName = "Units_config", menuName = "Battle/Units config")]
    public class UnitsConfig : ScriptableObject
    {
        public GameObject Prefab;
    }
}