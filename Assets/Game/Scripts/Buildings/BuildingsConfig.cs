using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Buildings_config", menuName = "Configs/Buildings config")]
    public class BuildingsConfig : ScriptableObject
    {
        public BuildingFactory CastleBuilding;

        public List<BuildingConfig> Buildings;
    }

    [Serializable]
    public class BuildingConfig
    {
        public Sprite Icon;
        public BuildingFactory BuildingFactory;
        public int Price;
    }
}