using System;
using System.Linq;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.Buildings
{
    public class ChooseBuildingPresenter : IDisposable
    {
        private IChooseBuildingView _chooseBuildingView;
        private BuildingsConfig _buildingsConfig;
        private CurrencyBank _currencyBank;
        private BuildingService _buildingService;

        private bool _isPanelOpen;
        private Vector3 _position;

        [Inject]
        public void Construct(
            IChooseBuildingView chooseBuildingView,
            BuildingsConfig buildingsConfig,
            CurrencyBank currencyBank,
            BuildingService buildingService)
        {
            _chooseBuildingView = chooseBuildingView;
            _buildingsConfig = buildingsConfig;
            _currencyBank = currencyBank;
            _buildingService = buildingService;
            
            _chooseBuildingView.OnCloseButtonClicked += OnCloseButtonClickedHandler;
            _chooseBuildingView.OnOptionChosen += OnOptionChosenHandler;
            _chooseBuildingView.OnClicked += OnClickedHandler;

            _currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
        }

        private void OnCloseButtonClickedHandler() => ClosePanel();

        private void OnOptionChosenHandler(int index)
        {
            var config = _buildingsConfig.Buildings.ElementAt(index);

            if (_buildingService.TryBuild(config, _position) == false)
                return;
            
            ClosePanel();
        }

        private void OnClickedHandler(Vector3 position)
        {
            if (_buildingService.IsPositionAvailable(position) == false)
                return;
            
            _position = position;
            
            _chooseBuildingView.ShowPointer(position);
            
            OpenPanel();
        }

        private void OnCurrencyChangedHandler(int obj) => Render();

        private void OpenPanel()
        {
            if (_isPanelOpen)
                return;
            
            _chooseBuildingView.Show();
            Render();

            _isPanelOpen = true;
        }

        private void ClosePanel()
        {
            if (!_isPanelOpen)
                return;
            
            _chooseBuildingView.Hide();
            _chooseBuildingView.HidePointer();
            
            _isPanelOpen = false;
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
            _chooseBuildingView.OnClicked -= OnClickedHandler;
        }
    }
}