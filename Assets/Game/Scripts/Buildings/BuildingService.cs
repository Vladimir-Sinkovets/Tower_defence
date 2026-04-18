using System.Collections;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class BuildingService : MonoBehaviour
    {
        private Registry<Building> _buildingRegistry;
        private CurrencyBank _currencyBank;
        private IInstantiator _instantiator;
        
        private Vector3 _position;
        private IEnumerator _coroutine;

        [Inject]
        public void Construct(
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank,
            IInstantiator instantiator)
        {
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
            _instantiator = instantiator;
        }

        public void SetPosition(Vector3 position) => _position = position;

        public bool IsPositionAvailable(Vector3 position)
        {
            foreach (var building in _buildingRegistry.All)
            {
                if (Vector3.Distance(building.transform.position, position) < building.RadiusOfOccupiedSpace)
                    return false;
            }

            return true;
        }

        public bool TryBuild(BuildingConfig config)
        {
            if (_currencyBank.TrySpend(config.Price) == false)
                return false;

            StartCoroutine(CreateBuilding(config, _position));
            
            return true;
        }
        

        private IEnumerator CreateBuilding(BuildingConfig buildingConfig, Vector2 position)
        {
            var building = buildingConfig.BuildingFactory.Create(_instantiator);

            building.transform.position = position;

            _coroutine = building.transform.PlayFallDownAppearanceAnimation();
            
            yield return _coroutine;
        }
        
        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}