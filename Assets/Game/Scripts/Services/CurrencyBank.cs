using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class CurrencyBank : IDisposable
    {
        public event Action<int> OnCurrencyChanged;

        private int _total;

        private readonly EnemyEvents _enemyEvents;

        public int Total
        {
            get => _total;
            private set
            {
                _total = value;

                OnCurrencyChanged?.Invoke(value);
            }
        }

        public CurrencyBank(EnemyEvents enemyEvents)
        {
            _enemyEvents = enemyEvents;

            _enemyEvents.OnEnemyDied += OnEnemyDiedHandler;

            Total = 2;
        }

        public bool TrySpend(int value)
        {
            if (value > Total)
                return false;

            Total -= value;

            return true;
        }

        private void OnEnemyDiedHandler()
        {
            Total += 1;

            Debug.Log($"Total - {Total}");
        }

        public void Dispose()
        {
            _enemyEvents.OnEnemyDied -= OnEnemyDiedHandler;
        }
    }
}