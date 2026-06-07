using Assets.Game.Scripts.UI.Currency;
using Assets.Game.Scripts.UI.HealthBar;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class HUD : MonoBehaviour
    {
        [field: SerializeField] public CurrencyView CurrencyView { get; private set; }
        [field: SerializeField] public HealthBarView HealthBarView { get; private set; }
    }
}