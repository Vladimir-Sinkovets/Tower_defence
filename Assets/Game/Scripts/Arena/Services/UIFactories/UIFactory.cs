using System;
using Assets.Game.Scripts.Arena.UI;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.HealthBar;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.UIFactories
{
    public class UIFactory : IUIFactory, IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly ArenaUIConfig _config;

        private HealthBarPresenter _castleHealthPresenter;
        
        public UIFactory(IInstantiator instantiator, ArenaUIConfig config)
        {
            _instantiator = instantiator;
            _config = config;
        }

        public void CreateHUD(Health playerHealth)
        {
            var hud = _instantiator.InstantiatePrefabForComponent<ArenaHUD>(_config.HUDPrefab);

            _castleHealthPresenter = _instantiator.Instantiate<HealthBarPresenter>(new object[] { hud.HealthBarView, playerHealth });
            _castleHealthPresenter.Init();
        }

        public ArenaInput CreateArenaInput() => 
            _instantiator.InstantiatePrefabForComponent<ArenaInput>(_config.InputPrefab);

        public void Dispose() => _castleHealthPresenter?.Dispose();
    }
}