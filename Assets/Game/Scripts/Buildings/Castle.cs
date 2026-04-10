using Assets.Game.Scripts.Shared;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class Castle : MonoBehaviour
    {
        public event Action OnCastleHpEnded;

        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Health _health;

        private BuildingsConfig _buildingsConfig;
        private DiContainer _container;

        [Inject]
        private void Construct(BuildingsConfig buildingsConfig, DiContainer container)
        {
            _buildingsConfig = buildingsConfig;
            _container = container;
        }

        public void Init()
        {
            var weapon = _buildingsConfig.CastleWeapon.Create(_container);

            weapon.transform.parent = _weaponPosition;
            weapon.transform.position = _weaponPosition.transform.position;

            _health.OnDied += OnDiedHandler;
        }

        private void OnDiedHandler() => OnCastleHpEnded?.Invoke();
    }
}