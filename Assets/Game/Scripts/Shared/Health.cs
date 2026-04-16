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
        
        private int _currentHp;

        public bool IsDead { get; private set; }

        private void Awake()
        {
            _currentHp = _startHp;
            IsDead = false;
        }

        public void ApplyDamage(int damage)
        {
            if (IsDead)
                return;

            _currentHp -= damage;

            OnHpChanged?.Invoke(_currentHp, _startHp);

            OnDamaged?.Invoke(damage);

            if (_currentHp <= 0)
            {
                IsDead = true;
                OnDied?.Invoke();
            }
        }

        public void ResetHealth()
        {
            IsDead = false;

            _currentHp = _startHp;

            OnHpChanged?.Invoke(_currentHp, _startHp);
        }
    }
}