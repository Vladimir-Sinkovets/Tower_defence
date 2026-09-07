using System;
using Assets.Game.Scripts.Arena.UI;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.HealthBar;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.HudFactories
{
    public class HudFactory : IHudFactory, IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly ArenaConfig _config;

        private HealthBarPresenter _castleHealthPresenter;
        
        public HudFactory(IInstantiator instantiator, ArenaConfig config)
        {
            _instantiator = instantiator;
            _config = config;
        }

        public void CreateHUD(Health playerHealth)
        {
            var hud = _instantiator.InstantiatePrefabForComponent<ArenaHUD>(_config.HUDPrefab.GetComponent<ArenaHUD>());

            _castleHealthPresenter = _instantiator.Instantiate<HealthBarPresenter>(new object[] { hud.HealthBarView, playerHealth });
            _castleHealthPresenter.Init();
        }

        public void Dispose() => _castleHealthPresenter?.Dispose();
    }
}