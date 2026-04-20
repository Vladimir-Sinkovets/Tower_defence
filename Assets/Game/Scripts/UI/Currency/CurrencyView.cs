using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Currency
{
    public class CurrencyView : MonoBehaviour, ICurrencyView 
    {
        [SerializeField] private TMP_Text _currencyText;

        public void SetCurrencyText(string text) => _currencyText.text = text;
    }
}