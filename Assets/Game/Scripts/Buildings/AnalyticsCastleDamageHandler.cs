using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class AnalyticsCastleDamageHandler : MonoBehaviour
    {
        private IGameplayAnalytics _gameplayAnalytics;
        private Health _health;

        [Inject]
        public void Construct(IGameplayAnalytics gameplayAnalytics) => _gameplayAnalytics = gameplayAnalytics;

        public void Init(Health health)
        {
            _health = health;

            _health.OnDamaged += OnDamageHandler;
        }

        private void OnDamageHandler(int obj) => _gameplayAnalytics.CastleDamaged();

        private void OnDestroy() => _health.OnDamaged -= OnDamageHandler;
    }
}