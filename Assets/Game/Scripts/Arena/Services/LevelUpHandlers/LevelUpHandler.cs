using System;
using Assets.Game.Scripts.Arena.Services.UpgradeServices;
using Assets.Game.Scripts.Arena.UI.Windows;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.LevelUpHandlers
{
    public class LevelUpHandler : IDisposable, IInitializable
    {
        private readonly IUpgradeService _upgradeService;
        private readonly IWindowsManager _windowsManager;

        public LevelUpHandler(IUpgradeService upgradeService, IWindowsManager windowsManager)
        {
            _upgradeService = upgradeService;
            _windowsManager = windowsManager;
        }

        public void Initialize() => _upgradeService.OnLevelUp += OnLevelUpHandler;

        private void OnLevelUpHandler() => _windowsManager.Open(WindowType.Upgrades);

        public void Dispose() => _upgradeService.OnLevelUp -= OnLevelUpHandler;
    }
}