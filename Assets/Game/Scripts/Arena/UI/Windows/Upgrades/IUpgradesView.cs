using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Services.UpgradeServices;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Arena.UI.Windows.Upgrades
{
    public interface IUpgradesView
    {
        event Action<Upgrade> OnUpgradeChosen;
        void ShowPanel();
        void UpdateUpgrades(IEnumerable<Upgrade> upgrades);
        UniTask HidePanel();
    }
}