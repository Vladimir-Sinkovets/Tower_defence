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
        private readonly GameDataHolder _gameDataHolder;

        public InAppPurchaseExecutor(ISaveService saveService, InAppPurchasesConfig config, GameDataHolder gameDataHolder)
        {
            _saveService = saveService;
            _config = config;
            _gameDataHolder = gameDataHolder;
        }

        public void Execute(string productId)
        {
            var product = _config.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                Debug.LogError($"Product {productId} not found");
                return;
            }
            
            product.Action.Execute(_saveService, _gameDataHolder);
        }
    }
}