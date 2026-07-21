using System;
using Assets.Game.Scripts.Services.Purchases;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Shop
{
    public class ShopButton : MonoBehaviour
    {
        public event Action<ProductItem> OnBuyButtonClicked;
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        private ProductItem _product;


        private void Awake() => _button.onClick.AddListener(OnClickedHandler);

        public void SetText(string text) => _text.text = text;
        public void SetProduct(ProductItem product) => _product = product;
        
        private void OnClickedHandler() => OnBuyButtonClicked?.Invoke(_product);

        private void OnDestroy() => _button.onClick.AddListener(OnClickedHandler);
    }
}