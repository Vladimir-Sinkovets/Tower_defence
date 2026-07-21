using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    [Serializable]
    public class ProductConfig
    {
        public string Id;
        [SerializeReference, SubclassSelector]
        public IPurchaseAction Action;
    }
}