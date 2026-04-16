using Assets.Game.Scripts.Buildings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class BuildingOption : MonoBehaviour
    {
        public event Action<BuildingOption> OnClick;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _unavailableImage;
        [SerializeField] private TMP_Text _price;

        private bool _availability;
        public BuildingConfig Config { get; private set; }

        public void Init(BuildingConfig config, bool availability)
        {
            Config = config;

            UpdateOption(availability);
        }

        public void OnClickHandler()
        {
            if (_availability)
                OnClick?.Invoke(this);
        }

        public void UpdateOption(bool availability)
        {
            _icon.sprite = Config.Icon;

            _availability = availability;

            _unavailableImage.gameObject.SetActive(!availability);

            _price.text = $"${Config.Price}";
        }

        private void OnDestroy()
        {
            OnClick = null;
        }
    }
}