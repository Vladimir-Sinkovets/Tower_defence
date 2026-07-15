using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Buildings_config", menuName = "Configs/Buildings config")]
    public class BuildingsConfig : ScriptableObject
    {
        public BuildingConfig CastleBuilding;

        public List<BuildingConfig> Buildings;
    }
}