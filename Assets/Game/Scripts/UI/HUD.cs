using Assets.Game.Scripts.UI.CastleHealth;
using Assets.Game.Scripts.UI.Currency;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class HUD : MonoBehaviour
    {
        [field: SerializeField] public CurrencyView CurrencyView { get; private set; }
        [field: SerializeField] public CastleHealthView CastleHealthView { get; private set; }
    }
}