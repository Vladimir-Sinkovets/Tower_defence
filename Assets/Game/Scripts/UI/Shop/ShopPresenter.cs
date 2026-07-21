using System;
using Assets.Game.Scripts.Services.Purchases;
using Zenject;

namespace Assets.Game.Scripts.UI.Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly IShopView _shopView;
        private readonly IInAppPurchaseManager _purchaseManager;

        public ShopPresenter(IShopView shopView, IInAppPurchaseManager purchaseManager)
        {
            _shopView = shopView;
            _purchaseManager = purchaseManager;
        }

        public void Initialize()
        {
            _shopView.OnOpenShopButtonClicked += OnOpenShopButtonClickedHandler;
            _shopView.OnCloseShopButtonClicked += OnCloseShopButtonClickedHandler;
            _shopView.OnBuyCoinsButtonClicked += OnBuyCoinsButtonClickedHandler;
            _purchaseManager.OnProductsChanged += OnProductsChangedHandler;
        }

        private void OnProductsChangedHandler() => RenderProducts();

        private void RenderProducts() => _shopView.SetProducts(_purchaseManager.Products);

        private void OnBuyCoinsButtonClickedHandler(ProductItem productItem) => _purchaseManager.BuyProduct(productItem);

        private void OnCloseShopButtonClickedHandler() => _shopView.Hide();
        private void OnOpenShopButtonClickedHandler()
        {
            _shopView.Show();

            RenderProducts();
        }

        public void Dispose()
        {
            _shopView.OnOpenShopButtonClicked -= OnOpenShopButtonClickedHandler;
            _shopView.OnCloseShopButtonClicked -= OnCloseShopButtonClickedHandler;
            _shopView.OnBuyCoinsButtonClicked -= OnBuyCoinsButtonClickedHandler;
            _purchaseManager.OnProductsChanged -= OnProductsChangedHandler;
        }
    }
}