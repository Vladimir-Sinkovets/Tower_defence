using System;
using UnityEngine;

namespace Assets.Game.Scripts.Shared
{
    public class Health : MonoBehaviour
    {
        public event Action<int, int> OnHpChanged;
        public event Action<int> OnDamaged;
        public event Action OnDied;

        [SerializeField] private int _startHp;

        private bool _isDead;

        public bool IsDead { get => _isDead; }

        private int _currentHp;

        private void Awake()
        {
            _currentHp = _startHp;
            _isDead = false;
        }

        public void ApplyDamage(int damage)
        {
            if (_isDead)
                return;

            _currentHp -= damage;

            OnHpChanged?.Invoke(_currentHp, _startHp);

            OnDamaged?.Invoke(damage);

            if (_currentHp <= 0)
            {
                _isDead = true;
                OnDied?.Invoke();
            }
        }

        public void ResetHealth()
        {
            _isDead = false;

            _currentHp = _startHp;

            OnHpChanged?.Invoke(_currentHp, _startHp);
        }
    }
}