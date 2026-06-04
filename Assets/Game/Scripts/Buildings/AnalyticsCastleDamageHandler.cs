using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class AnalyticsCastleDamageHandler : MonoBehaviour
    {
        private IGameAnalytics _gameAnalytics;
        private Health _health;

        [Inject]
        public void Construct(IGameAnalytics gameAnalytics) => _gameAnalytics = gameAnalytics;

        public void Init(Health health)
        {
            _health = health;

            _health.OnDamaged += OnDamageHandler;
        }

        private void OnDamageHandler(int obj) => _gameAnalytics.CastleDamaged();

        private void OnDestroy() => _health.OnDamaged -= OnDamageHandler;
    }
}