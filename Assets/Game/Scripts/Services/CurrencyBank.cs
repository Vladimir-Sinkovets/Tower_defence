using System;

namespace Assets.Game.Scripts.Services
{
    public class CurrencyBank
    {
        public event Action<int> OnCurrencyChanged;

        private int _total;

        public int Total
        {
            get => _total;
            private set
            {
                _total = value;

                OnCurrencyChanged?.Invoke(value);
            }
        }

        public bool TrySpend(int value)
        {
            if (value > Total)
                return false;

            Total -= value;

            return true;
        }

        public void Add(int value)
        {
            Total += value;
        }

        private void OnEnemyDiedHandler() => Total += 1;
    }
}