using Assets.Game.Scripts.Shared;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public class Castle : MonoBehaviour
    {
        public event Action OnHpEnded;
        public event Action OnDamaged;

        [field: SerializeField] public Transform BuildingPosition { get; private set; }
        [SerializeField] private Health _health;

        public void Init()
        {
            _health.OnDied += OnDiedHandler;
            _health.OnDamaged += OnDamagedHandler;
        }

        private void OnDamagedHandler(int _) => OnDamaged?.Invoke();

        private void OnDiedHandler() => OnHpEnded?.Invoke();

        private void OnDestroy()
        {
            _health.OnDied -= OnDiedHandler;
            _health.OnDamaged -= OnDamagedHandler;
        }
    }
}