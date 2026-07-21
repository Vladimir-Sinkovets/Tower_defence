using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services.Purchases;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Shop
{
    public class ShopView : MonoBehaviour, IShopView
    {
        public event Action OnOpenShopButtonClicked;
        public event Action OnCloseShopButtonClicked;
        public event Action<ProductItem> OnBuyCoinsButtonClicked;
        
        [SerializeField] private PanelAppearanceAnimation _panelAppearanceAnimation;
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _container;
        [SerializeField] private ShopButton _buttonPrefab;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private readonly List<ShopButton> _buttons = new List<ShopButton>();
        
        private void Awake()
        {
            _openButton.onClick.AddListener(OnOpenShopButtonClickedHandler);
            _closeButton.onClick.AddListener(OnCloseShopButtonClickedHandler);
        }

        private void OnCloseShopButtonClickedHandler() => OnCloseShopButtonClicked?.Invoke();
        private void OnOpenShopButtonClickedHandler() => OnOpenShopButtonClicked?.Invoke();

        public void Show()
        {
            _panel.SetActive(true);
            _panelAppearanceAnimation.Show();
        }

        public async UniTask Hide()
        {
            await _panelAppearanceAnimation.Hide();
            _panel.SetActive(false);
        }

        public void SetProducts(IEnumerable<ProductItem> purchaseManagerProducts)
        {
            foreach (var button in _buttons)
            {
                button.OnBuyButtonClicked -= OnBuyButtonClickedHandler;
                
                Destroy(button.gameObject);
            }
            
            _buttons.Clear();
            
            foreach (var product in purchaseManagerProducts)
            {
                if (product.IsOwned)
                    continue;
                
                var button = Instantiate(_buttonPrefab, _container);
                
                var title = product.Title;
                
                button.SetProduct(product);
                button.SetText(title);

                button.OnBuyButtonClicked += OnBuyButtonClickedHandler;
                
                _buttons.Add(button);
            }
        }

        private void OnBuyButtonClickedHandler(ProductItem productItem) => OnBuyCoinsButtonClicked?.Invoke(productItem);

        private void OnDestroy()
        {
            foreach (var button in _buttons)
            {
                button.OnBuyButtonClicked -= OnBuyButtonClickedHandler;
            }
            
            _openButton.onClick.RemoveListener(OnOpenShopButtonClickedHandler);
            _closeButton.onClick.RemoveListener(OnCloseShopButtonClickedHandler);
        }
    }
}