using System;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services.PointerRouters
{
    public class PointerRouter : IDisposable, IInitializable
    {
        private readonly PointSelector _pointSelector;
        private readonly IWindowsManager _windowsManager;
        private readonly IBuildingService _buildingService;

        public PointerRouter(PointSelector pointSelector, IWindowsManager windowsManager, IBuildingService buildingService)
        {
            _pointSelector = pointSelector;
            _windowsManager = windowsManager;
            _buildingService = buildingService;
        }
        
        public void Initialize() => _pointSelector.OnClicked += OnClickedHandler;

        private void OnClickedHandler(Vector3 position)
        {
            if (_buildingService.IsPositionAvailable(position))
                _windowsManager.Open(WindowType.Buildings).Forget();
        }

        public void Dispose() => _pointSelector.OnClicked -= OnClickedHandler;
    }
}