using System;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public delegate void HpChangedDelegate(int currentHp, int maxHp);

    public class Health : MonoBehaviour
    {
        public event HpChangedDelegate OnHpChanged;
        public event Action OnDied;

        [SerializeField] private int _startHp;

        private int _currentHp;

        private void Start()
        {
            _currentHp = _startHp;
        }

        public void GetDamage(int damage)
        {
            _currentHp -= damage;

            OnHpChanged?.Invoke(_currentHp, _startHp);

            if (_currentHp <= 0)
                OnDied?.Invoke();
        }
    }
}