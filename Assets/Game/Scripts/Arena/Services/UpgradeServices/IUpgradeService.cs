using System;
using System.Collections.Generic;
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
    }
}