using System;
using System.Linq;
using Assets.Game.Scripts.Arena.Services.ArenaConstantUpdateService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class ArenaUpgradePanelPresenter : IInitializable, IDisposable
    {
        private readonly IArenaUpgradePanelView _upgradePanelView;
        private readonly IArenaConstantUpgradeService _arenaConstantUpgradeService;

        public ArenaUpgradePanelPresenter(IArenaUpgradePanelView upgradePanelView, IArenaConstantUpgradeService arenaConstantUpgradeService)
        {
            _upgradePanelView = upgradePanelView;
            _arenaConstantUpgradeService = arenaConstantUpgradeService;
        }

        public void Initialize()
        {
            _upgradePanelView.OnCloseButtonClicked += OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked += OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked += OnUpgradeClickedHandler;
            
            _arenaConstantUpgradeService.OnUpgradesChanged += OnArenaConstantUpgradesChangedHandler;
            
            _upgradePanelView.Init();
        }

        private void Render()
        {
            var upgrades = _arenaConstantUpgradeService.GetUpgrades();
            
            var viewModels = upgrades
                .Select(upgrade => new ArenaUpgradePanelViewModel()
                {
                    Name = upgrade.Name,
                    Level = _arenaConstantUpgradeService.GetLevel(upgrade),
                    Cost = _arenaConstantUpgradeService.GetLevelCost(upgrade),
                    Icon = _arenaConstantUpgradeService.GetIcon(upgrade.Id),
                    IsAvailable = _arenaConstantUpgradeService.IsAvailable(upgrade),
                    Upgrade = $"+{upgrade.Upgrade}{upgrade.Unit}",
                    Id = upgrade.Id,
                }).ToList();
            
            _upgradePanelView.UpdateUpgradeList(viewModels);
        }

        private void OnUpgradeClickedHandler(string id)
        {
            var upgrade = _arenaConstantUpgradeService.GetUpgrade(id);
            
            if (!_arenaConstantUpgradeService.IsAvailable(upgrade))
                return;

            _arenaConstantUpgradeService.BuyUpgrade(upgrade);
        }

        private void OnOpenButtonClickedHandler()
        {
            Render();
            
            _upgradePanelView.ShowPanel();
        }

        private void OnArenaConstantUpgradesChangedHandler() => Render();

        private void OnCloseButtonClickedHandler() => _upgradePanelView.ClosePanel().Forget();
        
        public void Dispose()
        {
            _upgradePanelView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked -= OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked -= OnUpgradeClickedHandler;
            
            _arenaConstantUpgradeService.OnUpgradesChanged -= OnArenaConstantUpgradesChangedHandler;
        }
    }

    public class ArenaUpgradePanelViewModel
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