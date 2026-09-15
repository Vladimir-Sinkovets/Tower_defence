using System;
using Assets.Game.Scripts.Arena.UI;
using Assets.Game.Scripts.Arena.UI.Experience;
using Assets.Game.Scripts.Arena.UI.GameInfo;
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
        private ExperiencePresenter _experiencePresenter;
        private GameInfoPresenter _gameInfoPresenter;

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
            
            _experiencePresenter = _instantiator.Instantiate<ExperiencePresenter>(new object[] { hud.ExperienceView });
            _experiencePresenter.Init();
            
            _gameInfoPresenter = _instantiator.Instantiate<GameInfoPresenter>(new object[] { hud.GameInfoView });
            _gameInfoPresenter.Init();
        }

        public ArenaInput CreateArenaInput() => 
            _instantiator.InstantiatePrefabForComponent<ArenaInput>(_config.InputPrefab);

        public void Dispose()
        {
            _castleHealthPresenter?.Dispose();
            _experiencePresenter?.Dispose();
        }
    }
}