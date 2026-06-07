using System;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Buildings
{
    public class AnalyticsCastleDamageHandler : IDisposable
    {
        private readonly IAnalytics _analytics;
        private readonly Health _health;

        public AnalyticsCastleDamageHandler(IAnalytics analytics, Health health)
        {
            _analytics = analytics;
            _health = health;
            
            _health.OnDamaged += OnDamageHandler;
        }

        private void OnDamageHandler(int obj) => _analytics.CastleDamaged();

        public void Dispose() => _health.OnDamaged -= OnDamageHandler;
    }
}