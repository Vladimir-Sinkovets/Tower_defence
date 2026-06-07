using System;
using UnityEngine;

namespace Assets.Game.Scripts.Shared
{
    public class Health
    {
        public event Action<int, int> OnHpChanged;
        public event Action<int> OnDamaged;
        public event Action OnDied;

        public Transform Transform => _transform;

        private readonly int _startHp;
        private readonly Transform _transform;
        private int _currentHp;

        public bool IsDead { get; private set; }

        public Health(int hp, Transform transform)
        {
            _startHp = hp;
            _transform = transform;
            _currentHp = hp;
            
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
    }
}