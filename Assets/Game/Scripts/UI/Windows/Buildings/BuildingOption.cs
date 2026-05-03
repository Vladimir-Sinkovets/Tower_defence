using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Windows.Buildings
{
    public class BuildingOption : MonoBehaviour
    {
        public event Action<BuildingOption> OnClick;

        [SerializeField] private Button _button;
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
            
            _button.onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            if (_isAvailable)
                OnClick?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClickHandler);
            OnClick = null;
        }
    }
}