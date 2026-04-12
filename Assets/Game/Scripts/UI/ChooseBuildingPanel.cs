using Assets.Game.Scripts.Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class ChooseBuildingPanel : MonoBehaviour
    {
        public event Action<BuildingConfig> OnOptionChosen;
        public event Action OnCloseButtonClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _optionsContainer;
        [SerializeField] private BuildingOption _buildingOptionPrefab;

        private List<BuildingOption> _options = new();

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

        public void Hide()
        {
            _panel.SetActive(false);

            ClearContainer();
        }

        public void OnCloseButtonClickedHandler() => OnCloseButtonClicked?.Invoke();

        private void InitializeOptions(IEnumerable<BuildingConfig> configs, int total)
        {
            ClearContainer();

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
            Hide();

            OnOptionChosen?.Invoke(option.Config);
        }

        private void ClearContainer()
        {
            if (_options == null)
                return;

            foreach (var option in _options)
            {
                option.OnClick -= OnOptionClickedHandler;

                Destroy(option.gameObject);
            }

            _options.Clear();
        }

        private void OnDestroy() => ClearContainer();
    }
}