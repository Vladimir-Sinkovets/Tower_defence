using System;
using Assets.Game.Scripts.Arena.Services.UpgradeServices;

namespace Assets.Game.Scripts.Arena.UI.Windows.Upgrades
{
    public class UpgradesPresenter : IDisposable, IWindowPresenter
    {
        private readonly IUpgradeService _upgradeService;
        private readonly IUpgradesView _view;
        private readonly IWindowsManager _windowManager;

        public UpgradesPresenter(IUpgradeService upgradeService, IUpgradesView view, IWindowsManager windowManager)
        {
            _upgradeService = upgradeService;
            _view = view;
            _windowManager = windowManager;
        }
        
        public void Activate()
        {
            _view.OnUpgradeChosen += OnUpgradeChosenHandler;
            
            _view.UpdateUpgrades(_upgradeService.GetUpgrades());
            
            _view.ShowPanel();
        }

        public void Deactivate()
        {
            _view.OnUpgradeChosen -= OnUpgradeChosenHandler;
            
            _view.HidePanel();
        }

        private void OnUpgradeChosenHandler(Upgrade upgrade)
        {
            _upgradeService.BuyUpgrade(upgrade);
            
            _view.UpdateUpgrades(_upgradeService.GetUpgrades());

            if (!_upgradeService.HasExperienceForNextLevel())
                _windowManager.Close(WindowType.Upgrades);
        }

        public void Dispose() => Deactivate();
    }
}