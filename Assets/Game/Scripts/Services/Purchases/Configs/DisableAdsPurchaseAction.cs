using System;
using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    [Serializable]
    public class DisableAdsPurchaseAction : IPurchaseAction
    {
        public void Execute(ISaveService saveService, GameDataHolder gameDataHolder)
        {
            gameDataHolder.Data.IsAdsDisabled = true;
            saveService.Save();
        }
    }
}