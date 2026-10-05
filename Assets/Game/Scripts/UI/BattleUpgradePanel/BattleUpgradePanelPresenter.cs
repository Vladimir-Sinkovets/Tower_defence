using System;
using System.Linq;
using Assets.Game.Scripts.Battle.Services.Upgrades;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class BattleUpgradePanelPresenter : IInitializable, IDisposable
    {
        private readonly IBattleUpgradePanelView _upgradePanelView;
        private readonly IUpgradeService _upgradeService;

        public BattleUpgradePanelPresenter(IBattleUpgradePanelView upgradePanelView, IUpgradeService upgradeService)
        {
            _upgradePanelView = upgradePanelView;
            _upgradeService = upgradeService;
        }

        public void Initialize()
        {
            _upgradePanelView.OnCloseButtonClicked += OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked += OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked += OnUpgradeClickedHandler;
            
            _upgradeService.OnUpgradesChanged += OnUpgradesChangedHandler;
            
            _upgradePanelView.Init();
        }

        private void Render()
        {
            var upgrades = _upgradeService.GetUpgrades();
            
            var viewModels = upgrades
                .Select(upgrade => new BattleUpgradePanelViewModel()
                {
                    Name = upgrade.Name,
                    Level = _upgradeService.GetLevel(upgrade),
                    Cost = _upgradeService.GetLevelCost(upgrade),
                    Icon = _upgradeService.GetIcon(upgrade.Id),
                    IsAvailable = _upgradeService.IsAvailable(upgrade),
                    Upgrade = $"+{upgrade.Upgrade}{upgrade.Unit}",
                    Id = upgrade.Id,
                }).ToList();
            
            _upgradePanelView.UpdateUpgradeList(viewModels);
        }

        private void OnUpgradeClickedHandler(string id)
        {
            var upgrade = _upgradeService.GetUpgrade(id);
            
            if (!_upgradeService.IsAvailable(upgrade))
                return;

            _upgradeService.BuyUpgrade(upgrade);
        }

        private void OnOpenButtonClickedHandler()
        {
            Render();
            
            _upgradePanelView.ShowPanel();
        }

        private void OnUpgradesChangedHandler() => Render();

        private void OnCloseButtonClickedHandler() => _upgradePanelView.ClosePanel().Forget();
        
        public void Dispose()
        {
            _upgradePanelView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked -= OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked -= OnUpgradeClickedHandler;
            
            _upgradeService.OnUpgradesChanged -= OnUpgradesChangedHandler;
        }
    }

    public class BattleUpgradePanelViewModel
    {
        public string Name;
        public int Cost;
        public string Upgrade;
        public Sprite Icon;
        public bool IsAvailable;
        public int Level;
        public string Id;
    }
}