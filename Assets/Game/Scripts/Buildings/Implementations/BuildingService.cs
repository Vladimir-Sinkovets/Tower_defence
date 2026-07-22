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
        private readonly BuildingCounter _buildingCounter;

        private CancellationTokenSource _cts;
        private readonly GameSettings _settings;

        public BuildingService(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IBuildingFactory buildingFactory,
            IAnalytics analytics,
            IWavesController wavesController,
            GameSettingsService gameSettingsService,
            BuildingCounter buildingCounter)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _buildingFactory = buildingFactory;
            _analytics = analytics;
            _wavesController = wavesController;
            _settings = gameSettingsService.Settings;
            _buildingCounter = buildingCounter;
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
            var buildingSetting = _settings.BuildingSettings.FirstOrDefault(s => s.Id == config.Id);
            
            if (buildingSetting == null)
                return false;
            
            if (!_currencyBank.TrySpend(buildingSetting.Price))
                return false;
            
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            
            CreateBuildingAsync(config, position, _cts.Token).Forget();

            _buildingCounter.Increment();
            
            _analytics.TowerBuilt(buildingSetting.Price, _wavesController.WavesNumber);
            
            return true;
        }
        
        private async UniTaskVoid CreateBuildingAsync(BuildingConfig buildingConfig, Vector3 position, CancellationToken ct)
        {
            var settings = _settings.BuildingSettings.FirstOrDefault(s => s.Id == buildingConfig.Id);
            
            var building = await _buildingFactory.CreateAsync(buildingConfig, settings, BuildingType.Tower);

            building.transform.position = position;

            await building.AppearanceAnimation.PlayAsync(ct);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}