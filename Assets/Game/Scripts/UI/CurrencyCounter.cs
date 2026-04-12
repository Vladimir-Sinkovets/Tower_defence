using Assets.Game.Scripts.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI
{
    public class CurrencyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;
        private CurrencyBank _currencyBank;

        [Inject]
        public void Construct(CurrencyBank currencyBank)
        {
            _currencyBank = currencyBank;

            currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
        }

        private void OnCurrencyChangedHandler(int total) => _currencyText.text = $"${total}";

        private void OnDestroy() => _currencyBank.OnCurrencyChanged -= OnCurrencyChangedHandler;
    }
}