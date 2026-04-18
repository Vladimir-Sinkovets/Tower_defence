using Assets.Game.Scripts.Buildings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Buildings
{
    public class BuildingOption : MonoBehaviour
    {
        public event Action<BuildingOption> OnClick;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _unavailableImage;
        [SerializeField] private TMP_Text _price;

        private bool _isAvailable;
        public BuildingConfig Config { get; private set; }

        public void Init(BuildingConfig config, bool isAvailable)
        {
            Config = config;

            _icon.sprite = Config.Icon;

            _isAvailable = isAvailable;

            _unavailableImage.gameObject.SetActive(!isAvailable);

            _price.text = $"${Config.Price}";
        }

        public void OnClickHandler()
        {
            if (_isAvailable)
                OnClick?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnClick = null;
        }
    }
}