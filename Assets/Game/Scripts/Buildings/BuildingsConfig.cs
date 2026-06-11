using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Buildings_config", menuName = "Configs/Buildings config")]
    public class BuildingsConfig : ScriptableObject
    {
        public BuildingConfig CastleBuilding;

        public int CastleHp = 50;

        public List<BuildingOptionConfig> Buildings;
    }

    [Serializable]
    public class BuildingOptionConfig
    {
        public Sprite Icon;
        public BuildingConfig BuildingConfig;
        public int Price;
    }
}