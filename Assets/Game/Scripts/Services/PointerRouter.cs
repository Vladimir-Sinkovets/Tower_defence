using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.UI.Windows;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class PointerRouter
    {
        private readonly PointSelector _pointSelector;
        private readonly IWindowsManager _windowsManager;
        private readonly BuildingService _buildingService;

        public PointerRouter(PointSelector pointSelector, IWindowsManager windowsManager, BuildingService buildingService)
        {
            _pointSelector = pointSelector;
            _windowsManager = windowsManager;
            _buildingService = buildingService;

            _pointSelector.OnClicked += OnClickedHandler;
        }

        private void OnClickedHandler(Vector3 position)
        {
            if (_buildingService.IsPositionAvailable(position))
                _windowsManager.Open(WindowType.Buildings);
        }
    }
}