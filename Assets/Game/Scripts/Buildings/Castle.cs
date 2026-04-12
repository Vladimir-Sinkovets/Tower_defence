using Assets.Game.Scripts.Shared;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public class Castle : MonoBehaviour
    {
        public event Action OnCastleHpEnded;

        [SerializeField] private Transform _buildingPosition;
        [SerializeField] private Health _health;

        public Transform BuildingPosition { get => _buildingPosition; }

        public void Init()
        {
            _health.OnDied += OnDiedHandler;
        }

        private void OnDiedHandler() => OnCastleHpEnded?.Invoke();
    }
}