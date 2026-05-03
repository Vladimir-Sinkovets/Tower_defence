using System;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Windows.Buildings
{
    public class ChooseBuildingPresenter : IDisposable, IWindowPresenter
    {
        private readonly IChooseBuildingView _chooseBuildingView;
        private readonly BuildingsConfig _buildingsConfig;
        private readonly CurrencyBank _currencyBank;
        private readonly BuildingService _buildingService;
        private readonly PointSelector _pointSelector;
        private readonly IWindowsManager _windowManager;

        private Vector3 _position;
        private CancellationTokenSource _closePanelCts;

        public ChooseBuildingPresenter(
            IChooseBuildingView chooseBuildingView,
            BuildingsConfig buildingsConfig,
            CurrencyBank currencyBank,
            BuildingService buildingService,
            PointSelector pointSelector,
            IWindowsManager windowManager)
        {
            _chooseBuildingView = chooseBuildingView;
            _buildingsConfig = buildingsConfig;
            _currencyBank = currencyBank;
            _buildingService = buildingService;
            _pointSelector = pointSelector;
            _windowManager = windowManager;
        }

        
        public void Activate()
        {
            _chooseBuildingView.OnCloseButtonClicked += OnCloseButtonClickedHandler;
            _chooseBuildingView.OnOptionChosen += OnOptionChosenHandler;
            _pointSelector.OnClicked += OnClickedHandler;
            _currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
            
            ShowPanel();
        }

        public void Deactivate()
        {
            _chooseBuildingView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _chooseBuildingView.OnOptionChosen -= OnOptionChosenHandler;
            _currencyBank.OnCurrencyChanged -= OnCurrencyChangedHandler;
            _pointSelector.OnClicked -= OnClickedHandler;

            HidePanel();
        }

        
        private void OnCloseButtonClickedHandler() => _windowManager.Close(WindowType.Buildings);

        private void OnOptionChosenHandler(int index)
        {
            var config = _buildingsConfig.Buildings.ElementAt(index);

            if (_buildingService.TryBuild(config, _position) == false)
                return;
            
            _windowManager.Close(WindowType.Buildings);
        }

        private void OnClickedHandler(Vector3 position)
        {
            if (_buildingService.IsPositionAvailable(position) == false)
                return;
            
            _position = position;
            
            _chooseBuildingView.ShowPointer(position);
        }

        private void OnCurrencyChangedHandler(int _) => Render();

        private void ShowPanel()
        {
            _closePanelCts?.Cancel();
            _closePanelCts?.Dispose();
            _closePanelCts = new CancellationTokenSource();
            
            OnClickedHandler(_pointSelector.LastPosition);
            
            _chooseBuildingView.ShowPanel();
            
            Render();
        }

        private void HidePanel()
        {
            _chooseBuildingView.HidePointer();

            _chooseBuildingView.HidePanel(_closePanelCts.Token).Forget();
        }

        private void Render()
        {
            var viewModels = _buildingsConfig.Buildings
                .Select((buildingConfig, index) => new BuildingOptionViewModel()
                {
                    Price = buildingConfig.Price,
                    Icon = buildingConfig.Icon,
                    Index = index,
                    IsAvailable = buildingConfig.Price <= _currencyBank.Total,
                }).ToList();
            
            _chooseBuildingView.Render(viewModels);
        }

        
        public void Dispose()
        {
            _chooseBuildingView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _chooseBuildingView.OnOptionChosen -= OnOptionChosenHandler;
            _currencyBank.OnCurrencyChanged -= OnCurrencyChangedHandler;
            _pointSelector.OnClicked -= OnClickedHandler;
            
            _closePanelCts?.Cancel();
            _closePanelCts?.Dispose();
        }
    }
}