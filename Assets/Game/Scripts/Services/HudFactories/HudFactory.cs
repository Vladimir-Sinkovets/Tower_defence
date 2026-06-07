using System;
using System.Threading;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Currency;
using Assets.Game.Scripts.UI.HealthBar;
using Zenject;

namespace Assets.Game.Scripts.Services.HudFactories
{
    public class HudFactory : IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly HUD _hudPrefab;
        
        private CurrencyPresenter _currencyPresenter;
        private HealthBarPresenter _castleHealthPresenter;
        
        private CancellationTokenSource _startGameCts;

        public HudFactory(IInstantiator instantiator, HUD hudPrefab)
        {
            _instantiator = instantiator;
            _hudPrefab = hudPrefab;
        }

        public void CreateHUD(Health castleHealth)
        {
            var hud = _instantiator.InstantiatePrefabForComponent<HUD>(_hudPrefab);

            _currencyPresenter = _instantiator.Instantiate<CurrencyPresenter>(new object[] { hud.CurrencyView });
            _castleHealthPresenter = _instantiator.Instantiate<HealthBarPresenter>(new object[] { hud.HealthBarView, castleHealth });
        }

        public void Dispose()
        {
            _currencyPresenter?.Dispose();
            _castleHealthPresenter?.Dispose();
        }
    }
}