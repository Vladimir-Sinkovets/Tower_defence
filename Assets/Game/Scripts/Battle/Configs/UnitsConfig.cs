using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    [CreateAssetMenu(fileName = "Units_config", menuName = "Battle/Units config")]
    public class UnitsConfig : ScriptableObject
    {
        public List<UnitConfig> Units;
    }

    [Serializable]
    public class UnitConfig
    {
        public string Id;
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
        
        [SerializeReference] public List<IComponentConfig> Components = new()
        {
            new HealthConfig()
        };
    }
}