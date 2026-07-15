using System;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Windows.Buildings
{
    public class ChooseBuildingPresenter : IDisposable, IWindowPresenter
    {
        private readonly IChooseBuildingView _chooseBuildingView;
        private readonly BuildingsConfig _buildingsConfig;
        private readonly CurrencyBank _currencyBank;
        private readonly IBuildingService _buildingService;
        private readonly PointSelector _pointSelector;
        private readonly IWindowsManager _windowManager;
        private readonly IAnalytics _analytics;
        private readonly GameSettings _gameSettings;

        private Vector3 _position;
        private CancellationTokenSource _closePanelCts;

        public ChooseBuildingPresenter(
            IChooseBuildingView chooseBuildingView,
            BuildingsConfig buildingsConfig,
            CurrencyBank currencyBank,
            IBuildingService buildingService,
            PointSelector pointSelector,
            IWindowsManager windowManager,
            IAnalytics analytics,
            GameSettingsService gameSettingsService)
        {
            _chooseBuildingView = chooseBuildingView;
            _buildingsConfig = buildingsConfig;
            _currencyBank = currencyBank;
            _buildingService = buildingService;
            _pointSelector = pointSelector;
            _windowManager = windowManager;
            _analytics = analytics;
            _gameSettings = gameSettingsService.GameSettings;
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
            {
                _analytics.BuildRejected();
                
                return;
            }
            
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
                    Price = _gameSettings.BuildingSettings[index].Price,
                    Icon = buildingConfig.Icon,
                    Index = index,
                    IsAvailable = _gameSettings.BuildingSettings[index].Price <= _currencyBank.Total,
                }).ToList();
            
            _chooseBuildingView.Render(viewModels);
        }

        
        public void Dispose()
        {
            Deactivate();
            
            _closePanelCts?.Cancel();
            _closePanelCts?.Dispose();
        }
    }
}