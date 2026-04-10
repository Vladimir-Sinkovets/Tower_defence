using Assets.Game.Scripts.Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class ChooseBuildingPanel : MonoBehaviour
    {
        public event Action<BuildingConfig> OnOptionChosen;

        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _optionsContainer;
        [SerializeField] private BuildingOption _buildingOptionPrefab;

        public void Open(IEnumerable<BuildingConfig> configs)
        {
            _panel.SetActive(true);

            InitializeOptions(configs);
        }

        private void Hide()
        {
            _panel.SetActive(false);
        }

        private void InitializeOptions(IEnumerable<BuildingConfig> configs)
        {
            ClearContainer();

            foreach (var config in configs)
            {
                var option = Instantiate(_buildingOptionPrefab, _optionsContainer);

                option.Init(config);

                option.OnClick += OnOptionClickedHandler;
            }
        }

        private void OnOptionClickedHandler(BuildingOption option)
        {
            OnOptionChosen?.Invoke(option.Config);

            Hide();
        }

        private void ClearContainer()
        {
            foreach (Transform child in _optionsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}