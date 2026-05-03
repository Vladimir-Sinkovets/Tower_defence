using System;
using System.Threading;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.CastleHealth;
using Assets.Game.Scripts.UI.Currency;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class HudFactory : IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly HUD _hudPrefab;
        
        private CurrencyPresenter _currencyPresenter;
        private CastleHealthPresenter _castleHealthPresenter;
        
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
            _castleHealthPresenter = _instantiator.Instantiate<CastleHealthPresenter>(new object[] { hud.CastleHealthView, castleHealth });
        }

        public void Dispose()
        {
            _currencyPresenter?.Dispose();
            _castleHealthPresenter?.Dispose();
        }
    }
}