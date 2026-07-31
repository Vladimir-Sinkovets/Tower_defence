using System;

namespace Assets.Game.Scripts.Services.CurrencyBanks
{
    public interface ICurrencyBank
    {
        event Action<int> OnCurrencyChanged;
        public int Total { get; }
        bool TrySpend(int value);
        void Add(int value);
    }
}