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

        private List<BuildingOption> _options;

        public void Open(IEnumerable<BuildingConfig> configs, int total)
        {
            _panel.SetActive(true);

            InitializeOptions(configs, total);
        }

        public void UpdatePanel(int total)
        {
            foreach (var option in _options)
            {
                option.UpdateOption(IsAvailable(total, option.Config));
            }
        }

        private void Hide()
        {
            _panel.SetActive(false);
        }

        private void InitializeOptions(IEnumerable<BuildingConfig> configs, int total)
        {
            ClearContainer();

            _options = new();

            foreach (var config in configs)
            {
                var option = Instantiate(_buildingOptionPrefab, _optionsContainer);

                var availability = IsAvailable(total, config);

                option.Init(config, availability);

                option.OnClick += OnOptionClickedHandler;

                _options.Add(option);
            }
        }

        private static bool IsAvailable(int total, BuildingConfig config)
        {
            return total >= config.Price;
        }

        private void OnOptionClickedHandler(BuildingOption option)
        {
            OnOptionChosen?.Invoke(option.Config);

            Hide();
        }

        private void ClearContainer()
        {
            if (_options == null)
                return;

            foreach (var option in _options)
            {
                Destroy(option.gameObject);
            }

            _options.Clear();
        }
    }
}