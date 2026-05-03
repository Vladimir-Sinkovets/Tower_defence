using System;
using System.Threading;
using Assets.Game.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class BuildingService : IDisposable
    {
        private readonly Registry<Building> _buildingRegistry;
        private readonly CurrencyBank _currencyBank;
        private readonly IInstantiator _instantiator;
        
        private CancellationTokenSource _cts;

        public BuildingService(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IInstantiator instantiator)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _instantiator = instantiator;
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
            if (_currencyBank.TrySpend(config.Price) == false)
                return false;
            
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            
            CreateBuilding(config, position, _cts.Token).Forget();
            
            return true;
        }

        
        private async UniTaskVoid CreateBuilding(BuildingConfig buildingConfig, Vector3 position, CancellationToken ct)
        {
            var building = buildingConfig.BuildingFactory.Create(_instantiator);

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