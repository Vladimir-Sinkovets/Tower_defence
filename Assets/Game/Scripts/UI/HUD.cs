using Assets.Game.Scripts.UI.Buildings;
using Assets.Game.Scripts.UI.CastleHealth;
using Assets.Game.Scripts.UI.Currency;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class HUD : MonoBehaviour
    {
        public CurrencyView CurrencyView;
        public ChooseBuildingView ChooseBuildingView;
        public EndGameView EndGameView;
        public CastleHealthView CastleHealthView;
    }
}