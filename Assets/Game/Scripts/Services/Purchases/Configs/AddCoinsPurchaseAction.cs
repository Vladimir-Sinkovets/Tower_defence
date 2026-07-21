using System;
using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    [Serializable]
    public class AddCoinsPurchaseAction : IPurchaseAction
    {
        public int Amount;
        
        public void Execute(ISaveService saveService)
        {
            saveService.MetaCurrency += Amount;
            
            saveService.Save();
        }
    }
}