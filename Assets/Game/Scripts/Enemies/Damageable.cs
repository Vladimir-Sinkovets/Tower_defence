using System;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Damageable : MonoBehaviour
    {
        public event Action OnDied;

        [SerializeField] protected Health Health;

        private bool _isDied;

        public bool IsDied { get => _isDied; }

        public void GetDamage(int damage) => Health.GetDamage(damage);

        protected virtual void Awake() => Health.OnDied += OnDiedHandler;
        protected virtual void OnDestroy() => Health.OnDied -= OnDiedHandler;

        private void OnDiedHandler()
        {
            _isDied = true;

            OnDied?.Invoke();
        }
    }
}