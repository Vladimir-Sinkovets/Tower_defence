using System;
using Assets.Game.Scripts.Services.Configs.Enemies;
using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;
        public event Action Activated;
        public event Action Deactivated;

        protected Health Health;

        public bool IsDead => Health.IsDead;
        
        public int Award { get; private set; }

        public void Activate() => Activated?.Invoke();

        public void Deactivate() => Deactivated?.Invoke();

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