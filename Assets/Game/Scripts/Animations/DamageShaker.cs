using Assets.Game.Scripts.Shared;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class DamageShaker : MonoBehaviour
    {
        private const float ShakeDuration = 0.1f;
        private const float ShakeStrength = 0.1f;
        private const int ShakeVibrato = 5;
        
        private Tween _shakeTween;
        private Health _health;
        private Transform _root;

        public void Init(Health health, Transform root)
        {
            _health = health;
            _health.OnDamaged += OnDamagedHandler;
            _root = root;
        }

        private void OnDamagedHandler(int _)
        {
            _shakeTween?.Complete();

            _shakeTween = _root.DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibrato);
        }

        private void OnDestroy()
        {
            _health.OnDamaged -= OnDamagedHandler;
            _shakeTween?.Kill();
        }
    }
}