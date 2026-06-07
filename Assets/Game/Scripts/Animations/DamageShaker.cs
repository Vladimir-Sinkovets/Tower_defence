using System;
using Assets.Game.Scripts.Shared;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class DamageShaker : IDisposable
    {
        private const float ShakeDuration = 0.1f;
        private const float ShakeStrength = 0.1f;
        private const int ShakeVibrato = 5;
        
        private readonly Health _health;
        private readonly Transform _root;
        
        private Tween _shakeTween;

        public DamageShaker(Health health, Transform root)
        {
            _health = health;
            _root = root;
            _health.OnDamaged += OnDamagedHandler;
        }

        private void OnDamagedHandler(int _)
        {
            _shakeTween?.Complete();

            _shakeTween = _root.DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibrato);
        }

        public void Dispose()
        {
            _health.OnDamaged -= OnDamagedHandler;
            _shakeTween?.Kill();
        }
    }
}