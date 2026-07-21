using System;
using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    [Serializable]
    public class DisableAdsPurchaseAction : IPurchaseAction
    {
        public void Execute(ISaveService saveService)
        {
            saveService.DisableAds();
            saveService.Save();
        }
    }
}