using Assets.Game.Scripts.Services;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI
{
    public class CurrencyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;

        [Inject]
        private void Construct(CurrencyBank currencyBank)
        {
            currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
        }

        private void OnCurrencyChangedHandler(int total)
        {
            _currencyText.text = $"${total}";
        }
    }
}