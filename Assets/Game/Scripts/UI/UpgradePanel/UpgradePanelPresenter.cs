using System;
using System.Linq;
using Assets.Game.Scripts.Upgrades.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class UpgradePanelPresenter : IInitializable, IDisposable
    {
        private readonly IUpgradePanelView _upgradePanelView;
        private readonly IUpgradeService _upgradeService;

        public UpgradePanelPresenter(IUpgradePanelView upgradePanelView, IUpgradeService upgradeService)
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

        private async UniTask RenderAsync()
        {
            var upgrades = await _upgradeService.GetUpgradesAsync();
            
            var viewModels = upgrades
                .Select(upgrade => new UpgradePanelViewModel()
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

        private void OnUpgradeClickedHandler(string id) => OnUpgradeClickedHandlerAsync(id).Forget();

        private async UniTask OnUpgradeClickedHandlerAsync(string id)
        {
            var upgrade = await _upgradeService.GetUpgrade(id);
            
            if (!_upgradeService.IsAvailable(upgrade))
                return;

            _upgradeService.BuyUpgradeAsync(upgrade).Forget();
        }

        private void OnOpenButtonClickedHandler()
        {
            RenderAsync().Forget();
            
            _upgradePanelView.ShowPanel();
        }

        private void OnUpgradesChangedHandler() => RenderAsync().Forget();

        private void OnCloseButtonClickedHandler() => _upgradePanelView.ClosePanel().Forget();
        
        public void Dispose()
        {
            _upgradePanelView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked -= OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked -= OnUpgradeClickedHandler;
            
            _upgradeService.OnUpgradesChanged -= OnUpgradesChangedHandler;
        }
    }

    public class UpgradePanelViewModel
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