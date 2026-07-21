using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    [CreateAssetMenu(menuName = "Services/Purchases")]
    public class InAppPurchasesConfig : ScriptableObject
    {
        public List<ProductConfig> Products;
    }
}