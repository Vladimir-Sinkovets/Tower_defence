using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.GameplayCurrency
{
    public class CurrencyView : MonoBehaviour, ICurrencyView 
    {
        [SerializeField] private TMP_Text _currencyText;

        public void SetCurrencyText(string text) => _currencyText.text = text;
    }
}