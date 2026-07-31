using System;
using Assets.Game.Scripts.Services.Configs.Enemies;
using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;

        protected Health Health;

        public bool IsActive { get; private set; }
        
        public bool IsDead => Health.IsDead;
        
        public int Award { get; private set; }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public virtual void Init(EnemySettings settings, Health targetHealth, Transform targetTransform)
        {
            Health = new Health(settings.Hp);
            Health.OnDied += OnDiedHandler;
            Award = settings.Award;
        }

        public void ApplyDamage(int damage) => Health.ApplyDamage(damage);
        
        protected virtual void OnDiedHandler() => OnDied?.Invoke(this);

        protected virtual void OnDestroy() => Health.OnDied -= OnDiedHandler;
    }
}