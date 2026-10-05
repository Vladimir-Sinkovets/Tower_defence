using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Views
{
    public class CurrencyView : MonoBehaviour, ICurrencyView
    {
        [SerializeField] private TMP_Text _currencyText;

        public void SetCurrency(int value) => _currencyText.text = value.ToString();
    }
}