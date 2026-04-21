using Assets.Game.Scripts.Shared;
using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public class Castle : MonoBehaviour
    {
        private const float ShakeDuration = 0.1f;
        private const float ShakeStrength = 0.1f;
        private const int ShakeVibrato = 5;
        
        public event Action OnHpEnded;

        [field: SerializeField] public Transform BuildingPosition { get; private set; }
        [SerializeField] private Health _health;

        private Tween _shakeTween;

        
        public void Init()
        {
            _health.OnDied += OnDiedHandler;
            _health.OnDamaged += OnDamagedHandler;
        }

        
        private void OnDamagedHandler(int _)
        {
            _shakeTween?.Complete();

            _shakeTween = transform.DOShakePosition(ShakeDuration, ShakeStrength, ShakeVibrato);
        }

        private void OnDiedHandler() => OnHpEnded?.Invoke();

        private void OnDestroy()
        {
            _health.OnDied -= OnDiedHandler;
            _health.OnDamaged -= OnDamagedHandler;
        }
    }
}