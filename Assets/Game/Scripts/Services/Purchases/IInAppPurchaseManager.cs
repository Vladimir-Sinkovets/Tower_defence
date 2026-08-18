using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Purchasing;

namespace Assets.Game.Scripts.Services.Purchases
{
    public interface IInAppPurchaseManager
    {
        event Action OnProductsChanged;
        UniTask InitializeAsync();
        IReadOnlyList<ProductItem> Products { get; }
        void BuyProduct(ProductItem product);
    }

    public class ProductItem
    {
        public string Id;
        public string Title;
        public bool IsOwned;
        public ProductType Type;
    }
}