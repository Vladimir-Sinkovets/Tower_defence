using System;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI.CastleHealth
{
    public class CastleHealthPresenter : IDisposable
    {
        private readonly CastleHealthView _castleHealthView;
        private readonly Health _castleHealth;

        public CastleHealthPresenter(CastleHealthView castleHealthView, Health castleHealth)
        {
            _castleHealthView = castleHealthView;
            _castleHealth = castleHealth;

            _castleHealth.OnHpChanged += OnHpChangedHandler;
        }
        
        private void OnHpChangedHandler(int currentHp, int maxHp) => _castleHealthView.UpdateHealthBar(currentHp / (float)maxHp);

        public void Dispose() => _castleHealth.OnHpChanged += OnHpChangedHandler;
    }
}