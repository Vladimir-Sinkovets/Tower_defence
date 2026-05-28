using System;
using System.Threading;
using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class BuildingService : IBuildingService, IDisposable
    {
        private readonly Registry<Building> _buildingRegistry;
        private readonly CurrencyBank _currencyBank;
        private readonly IBuildingFactory _buildingFactory;
        private readonly IGameplayAnalytics _gameplayAnalytics;
        private readonly IWavesController _wavesController;

        private CancellationTokenSource _cts;
        
        private int _towersCount;

        public BuildingService(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IBuildingFactory buildingFactory,
            IGameplayAnalytics gameplayAnalytics,
            IWavesController wavesController)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _buildingFactory = buildingFactory;
            _gameplayAnalytics = gameplayAnalytics;
            _wavesController = wavesController;
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

        public bool TryBuild(BuildingOptionConfig optionConfig, Vector3 position)
        {
            if (_currencyBank.TrySpend(optionConfig.Price) == false)
                return false;
            
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            
            CreateBuilding(optionConfig, position, _cts.Token).Forget();

            _towersCount++;
            
            _gameplayAnalytics.TowerBuilt(optionConfig.Price, _towersCount, _wavesController.WavesCount);
            
            return true;
        }
        
        private async UniTaskVoid CreateBuilding(BuildingOptionConfig buildingOptionConfig, Vector3 position, CancellationToken ct)
        {
            var building = _buildingFactory.Create(buildingOptionConfig.BuildingConfig);

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