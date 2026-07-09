using System;
using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action OnDied;

        protected Health Health;

        public bool IsActive { get; private set; }
        
        public bool IsDead => Health.IsDead;
        
        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public virtual void Init(EnemyConfig config, Health targetHealth, Transform targetTransform)
        {
            Health = new Health(config.Hp);
            Health.OnDied += OnDiedHandler;
        }

        public void ApplyDamage(int damage) => Health.ApplyDamage(damage);
        
        protected virtual void OnDiedHandler() => OnDied?.Invoke();

        protected void OnDestroy() => Health.OnDied -= OnDiedHandler;
    }
}