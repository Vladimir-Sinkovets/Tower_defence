using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private Transform _planeCenter;

        private GameInput _input;
        private ChooseBuildingPanel _chooseBuildingPanel;
        private BuildingsConfig _buildingsConfig;
        private BuildingBuilder _builder;
        private Registry<Building> _buildingRegistry;
        private CurrencyBank _currencyBank;
        private Camera _mainCamera;

        [Inject]
        private void Construct(
            GameInput input,
            ChooseBuildingPanel chooseBuildingPanel,
            BuildingsConfig buildingsConfig,
            BuildingBuilder builder,
            Registry<Building> buildingRegistry,
            CurrencyBank currencyBank)
        {
            _mainCamera = Camera.main;
            _input = input;

            _chooseBuildingPanel = chooseBuildingPanel;
            _buildingsConfig = buildingsConfig;
            _builder = builder;
            _buildingRegistry = buildingRegistry;
            _currencyBank = currencyBank;
        }

        public void Init()
        {
            _input.Tap += OnTapHandler;

            _chooseBuildingPanel.OnOptionChosen += OnOptionChosenHandler;

            _currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
        }

        private void OnCurrencyChangedHandler(int total)
        {
            _chooseBuildingPanel.UpdatePanel(total);
        }

        private void OnTapHandler(Vector2 tapPosition)
        {
            if (IsPointOverUI(tapPosition))
                return;

            var position = GetPoint(tapPosition);

            if (IsPositionAvailable(position) == false)
                return;

            StartBuilding(position);
        }

        private bool IsPointOverUI(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            Debug.Log(results.Count > 0);

            return results.Count > 0;
        }

        public void StartBuilding(Vector3 position)
        {
            _builder.Clean();

            _builder.SetPosition(position);

            _chooseBuildingPanel.Open(_buildingsConfig.Buildings, _currencyBank.Total);
        }

        private void OnOptionChosenHandler(BuildingConfig buildingConfig)
        {
            var isSucceeded = _currencyBank.TrySpend(buildingConfig.Price);

            if (isSucceeded == false)
                return;

            _builder.SetBuilding(buildingConfig);
            _builder.Build();
        }

        private bool IsPositionAvailable(Vector3 position)
        {
            foreach (var building in _buildingRegistry.All)
            {
                if (Vector3.Distance(building.transform.position, position) < building.RadiusOfOccupiedSpace)
                    return false;
            }

            return true;
        }

        private Vector3 GetPoint(Vector2 tapPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(tapPosition);

            var buildPlane = new Plane(Vector3.up, _planeCenter.position);

            if (buildPlane.Raycast(ray, out var enter))
            {
                return ray.GetPoint(enter);
            }

            return Vector3.zero;
        }

        private void OnDestroy()
        {
            _input.Tap -= OnTapHandler;
        }
    }
}