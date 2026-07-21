using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Purchases.Configs;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Purchases
{
    public class InAppPurchaseExecutor : IInAppPurchaseExecutor
    {
        private readonly ISaveService _saveService;
        private readonly InAppPurchasesConfig _config;

        public InAppPurchaseExecutor(ISaveService saveService, InAppPurchasesConfig config)
        {
            _saveService = saveService;
            _config = config;
        }

        public void Execute(string productId)
        {
            var product = _config.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                Debug.LogError($"Product {productId} not found");
                return;
            }
            
            product.Action.Execute(_saveService);
        }
    }
}