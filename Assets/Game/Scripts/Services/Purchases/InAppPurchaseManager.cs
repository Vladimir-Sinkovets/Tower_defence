using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Assets.Game.Scripts.Services.Purchases
{
    public class InAppPurchaseManager : IInAppPurchaseManager, IDisposable
    {
        public event Action OnProductsChanged;
        
        private readonly IInAppPurchaseExecutor _executor;
        private StoreController _controller;

        private List<ProductItem> _products = new();
        private readonly List<string> _ownedProducts = new();
        private bool _isInitialized;

        public IReadOnlyList<ProductItem> Products => _products;

        public InAppPurchaseManager(IInAppPurchaseExecutor executor) => _executor = executor;

        public void BuyProduct(ProductItem product)
        {
            if (!_isInitialized)
            {
                Debug.LogWarning($"[{nameof(InAppPurchaseManager)}] Store is not initialized");
                return;
            }
            
            if (product == null)
            {
                Debug.LogError($"[{nameof(InAppPurchaseManager)}] BuyProduct: null");
                return;
            }
            
            _controller.PurchaseProduct(product.Id);
        }

        public async UniTask InitializeAsync()
        {
            _controller = UnityIAPServices.StoreController();
            
            SubscribeIAPEvents();
            
            await _controller.Connect();
            
            var initialProductToFetch = BuildProductDefinitions();;
            
            _controller.FetchProducts(initialProductToFetch);
        }
        
        private List<ProductDefinition> BuildProductDefinitions()
        {
            return ProductCatalog.LoadDefaultCatalog().allProducts
                .Select(productCatalogItem => 
                    new ProductDefinition(productCatalogItem.id, productCatalogItem.type))
                .ToList();
        }

        private void SubscribeIAPEvents()
        {
            if (_controller == null) return;

            _controller.OnProductsFetched += OnProductsFetched;
            _controller.OnProductsFetchFailed += OnProductsFetchFailed;

            _controller.OnPurchasesFetched += OnPurchasesFetched;
            _controller.OnPurchasesFetchFailed += OnPurchasesFetchFailed;

            _controller.OnPurchasePending += OnPurchasePending;       
            _controller.OnPurchaseConfirmed += OnPurchaseConfirmed;
            _controller.OnPurchaseFailed += OnPurchaseFailed;         

            _controller.OnStoreDisconnected += OnStoreDisconnected;
        }
        
        private void UnsubscribeIAPEvents()
        {
            if (_controller == null) return;

            _controller.OnProductsFetched -= OnProductsFetched;
            _controller.OnProductsFetchFailed -= OnProductsFetchFailed;

            _controller.OnPurchasesFetched -= OnPurchasesFetched;
            _controller.OnPurchasesFetchFailed -= OnPurchasesFetchFailed;

            _controller.OnPurchasePending -= OnPurchasePending;
            _controller.OnPurchaseConfirmed -= OnPurchaseConfirmed;
            _controller.OnPurchaseFailed -= OnPurchaseFailed;

            _controller.OnStoreDisconnected -= OnStoreDisconnected;
        }

        #region Events handlers
        private void OnPurchaseConfirmed(Order order)
        {
            Debug.Log($"[{nameof(InAppPurchaseManager)}] Purchase confirmed");

            if (order?.Info?.PurchasedProductInfo == null || order.Info.PurchasedProductInfo.Count <= 0)
                return;
            
            var productId = order.Info.PurchasedProductInfo[0].productId;

            _executor.Execute(productId);
            
            _controller.FetchPurchases();
        }

        private void OnPurchasePending(PendingOrder order)
        {
            Debug.Log($"[{nameof(InAppPurchaseManager)}] Pending Order: {order}");
            
            _controller.ConfirmPurchase(order);
        }

        private void OnProductsFetched(List<Product> products)
        {
            LogProductsFetched(products);
            
            _products = products
                .Select(x => new ProductItem()
                {
                    Id = x.definition.id,
                    Title = x.metadata.localizedTitle,
                    Type = x.type,
                    IsOwned = false,
                })
                .ToList();

            OnProductsChanged?.Invoke();
            
            _controller.FetchPurchases();
            
            _isInitialized = true;
        }

        private void LogProductsFetched(List<Product> products)
        {
            Debug.Log($"[{nameof(InAppPurchaseManager)}] Products fetched: {products.Count}");
            foreach (var p in products)
            {
                Debug.Log($"[{nameof(InAppPurchaseManager)}] {p.definition.id} | {p.metadata.localizedTitle} | {p.metadata.localizedPriceString}");
            }
        }

        private void OnPurchaseFailed(FailedOrder order) => Debug.LogError($"[{nameof(InAppPurchaseManager)}] Failed Order: {order}");
        
        private void OnProductsFetchFailed(ProductFetchFailed failure) => Debug.LogError($"[{nameof(InAppPurchaseManager)}] Product fetch failed: {failure.FailureReason}");
        
        private void OnPurchasesFetched(Orders orders)
        {
            Debug.Log($"[{nameof(InAppPurchaseManager)}] Purchase fetched");
            
            _ownedProducts.Clear();
            
            foreach (var order in orders.ConfirmedOrders)
            {
                foreach (var product in order.Info.PurchasedProductInfo)
                {
                    _ownedProducts.Add(product.productId);
                }
            }

            foreach (var product in _products)
            {
                product.IsOwned = product.Type == ProductType.NonConsumable && _ownedProducts.Contains(product.Id);
            }
            
            OnProductsChanged?.Invoke();
        }

        private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure) => Debug.LogError($"[{nameof(InAppPurchaseManager)}] Purchases fetch failed: {failure.FailureReason}");
        private void OnStoreDisconnected(StoreConnectionFailureDescription desc) => Debug.LogError($"[{nameof(InAppPurchaseManager)}] Store disconnected: {desc.Message}");
        #endregion
        
        public void Dispose() => UnsubscribeIAPEvents();
    }
}