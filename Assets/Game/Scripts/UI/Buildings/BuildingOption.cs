using Assets.Game.Scripts.Buildings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Buildings
{
    public class BuildingOption : MonoBehaviour
    {
        public event Action<BuildingOption> OnClick;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _unavailableImage;
        [SerializeField] private TMP_Text _price;

        private bool _isAvailable;
        public int Index { get; private set; }

        public void Init(Sprite icon, int index, int price, bool isAvailable)
        {
            Index =  index;
            
            _icon.sprite = icon;

            _isAvailable = isAvailable;

            _unavailableImage.gameObject.SetActive(!isAvailable);

            _price.text = $"${price}";
        }

        public void OnClickHandler()
        {
            if (_isAvailable)
                OnClick?.Invoke(this);
        }

        private void OnDestroy() => OnClick = null;
    }
}