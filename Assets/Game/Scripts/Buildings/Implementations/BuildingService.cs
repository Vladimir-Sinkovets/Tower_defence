using System;
using System.Linq;
using System.Threading;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.Registries;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class BuildingService : IBuildingService, IDisposable
    {
        private readonly Registry<Building> _buildingRegistry;
        private readonly CurrencyBank _currencyBank;
        private readonly IBuildingFactory _buildingFactory;
        private readonly IAnalytics _analytics;
        private readonly IWavesController _wavesController;
        private readonly GameSettings _gameSettings;

        private CancellationTokenSource _cts;
        
        private int _towersCount;

        public BuildingService(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IBuildingFactory buildingFactory,
            IAnalytics analytics,
            IWavesController wavesController,
            GameSettingsService gameSettingsService)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _buildingFactory = buildingFactory;
            _analytics = analytics;
            _wavesController = wavesController;
            _gameSettings = gameSettingsService.GameSettings;
        }
        
        
        public bool IsPositionAvailable(Vector3 position)
        {
            foreach (var building in _buildingRegistry.All)
            {
                if (Vector3.Distance(building.transform.position, position) < building.RadiusOfOccupiedSpace)
                    return false;
            }

            return true;
        }

        public bool TryBuild(BuildingConfig config, Vector3 position)
        {
            var price = _gameSettings.BuildingSettings.FirstOrDefault(s => s.Id == config.Id).Price;
            
            if (_currencyBank.TrySpend(price) == false)
                return false;
            
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            
            CreateBuilding(config, position, _cts.Token).Forget();

            _towersCount++;
            
            _analytics.TowerBuilt(price, _towersCount, _wavesController.WavesCount);
            
            return true;
        }
        
        private async UniTaskVoid CreateBuilding(BuildingConfig buildingConfig, Vector3 position, CancellationToken ct)
        {
            var settings = _gameSettings.BuildingSettings.FirstOrDefault(s => s.Id == buildingConfig.Id);
            
            var building = await _buildingFactory.Create(buildingConfig, settings, BuildingType.Tower);

            building.transform.position = position;

            await building.AppearanceAnimation.Play(ct);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}