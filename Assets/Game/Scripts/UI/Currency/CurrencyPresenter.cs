using System;
using Assets.Game.Scripts.Services;

namespace Assets.Game.Scripts.UI.Currency
{
    public class CurrencyPresenter : IDisposable
    {
        private readonly CurrencyBank _currencyBank;
        private readonly ICurrencyView _view;

        public CurrencyPresenter(CurrencyBank currencyBank, ICurrencyView view)
        {
            _currencyBank =  currencyBank;
            _view = view;

            _currencyBank.OnCurrencyChanged += OnCurrencyChangedHandler;
            
            _view.SetCurrencyText($"${_currencyBank.Total}");
        }

        private void OnCurrencyChangedHandler(int total) => _view.SetCurrencyText($"${total}");

        public void Dispose() => _currencyBank.OnCurrencyChanged -= OnCurrencyChangedHandler;
    }
}