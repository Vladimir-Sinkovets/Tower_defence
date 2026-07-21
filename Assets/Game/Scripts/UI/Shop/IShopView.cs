using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Services.Purchases;
using Cysharp.Threading.Tasks;
using UnityEngine.Purchasing;

namespace Assets.Game.Scripts.UI.Shop
{
    public interface IShopView
    {
        event Action OnOpenShopButtonClicked;
        event Action OnCloseShopButtonClicked;
        event Action<ProductItem> OnBuyCoinsButtonClicked;
        void Show();
        UniTask Hide();
        void SetProducts(IEnumerable<ProductItem> purchaseManagerProducts);
    }
}