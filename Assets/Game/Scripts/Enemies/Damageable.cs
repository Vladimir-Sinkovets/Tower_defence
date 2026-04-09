using Assets.Game.Scripts.Services;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Damageable : MonoBehaviour
    {
        public event Action OnDied;

        [SerializeField] protected Health Health;

        private bool _isDead;

        public bool IsDied { get => _isDead; }

        public void GetDamage(int damage) => Health.GetDamage(damage);

        protected virtual void Awake() => Health.OnDied += OnHealthDiedHandler;
        protected virtual void OnDestroy() => Health.OnDied -= OnHealthDiedHandler;

        private void OnHealthDiedHandler()
        {
            if (_isDead)
                return;

            OnDied?.Invoke();

            _isDead = true;
        }
    }
}