using System.Collections;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class BuildingService
    {
        private Registry<Building> _buildingRegistry;
        private CurrencyBank _currencyBank;
        private IInstantiator _instantiator;
        private ICoroutineRunner _coroutineRunner;

        [Inject]
        public void Construct(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IInstantiator instantiator,
            ICoroutineRunner coroutineRunner)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _instantiator = instantiator;
            _coroutineRunner = coroutineRunner;
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

            _coroutineRunner.Run(CreateBuilding(config, position));
            
            return true;
        }

        
        private IEnumerator CreateBuilding(BuildingConfig buildingConfig, Vector3 position)
        {
            var building = buildingConfig.BuildingFactory.Create(_instantiator);

            building.transform.position = position;

            yield return building.transform.PlayFallDownAppearanceAnimation();
        }
    }
}