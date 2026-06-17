using System;
using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action OnDied;

        protected Health Health;
        
        public bool IsDead => Health.IsDead;

        public virtual void Activate(Health targetHealth, Transform targetTransform) => Health.OnDied += OnDiedHandler;

        public virtual void Deactivate() => Health.OnDied -= OnDiedHandler;
        
        public abstract void Init(EnemyConfig config);

        public void ApplyDamage(int damage) => Health.ApplyDamage(damage);
        
        private void OnDiedHandler() => OnDied?.Invoke();
    }
}