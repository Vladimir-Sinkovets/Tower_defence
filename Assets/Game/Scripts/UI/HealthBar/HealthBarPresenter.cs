using System;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI.HealthBar
{
    public class HealthBarPresenter : IDisposable
    {
        private readonly Health _health;
        private readonly IHealthBarView _view;

        public HealthBarPresenter(Health health, IHealthBarView view)
        {
            _health = health;
            _view = view;
            
            _health.OnHpChanged += OnHpChangedHandler;
        }

        private void OnHpChangedHandler(int currentHp, int maxHp) => _view.UpdateBar(currentHp / (float)maxHp);

        public void Dispose() => _health.OnHpChanged -= OnHpChangedHandler;
    }
}