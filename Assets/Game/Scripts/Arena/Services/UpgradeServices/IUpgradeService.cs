using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Services.Experiences;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.UpgradeServices
{
    public interface IUpgradeService
    {
        event Action OnLevelUp;
        IEnumerable<Upgrade> GetUpgrades();
        void BuyUpgrade(Upgrade upgrade);
        bool HasExperienceForNextLevel();
    }

    public class Upgrade
    {
        public string Name;
        public Sprite Icon;
        public int Level;
        public UpgradeType Type;
        public float EachLevelCoefficient = 1.0f;
    }
}