using System;
using Assets.Game.Scripts.Battle.UI;
using Assets.Game.Scripts.Battle.UI.StartPanel;
using Assets.Game.Scripts.Battle.UI.UnitsPanel;
using Zenject;

namespace Assets.Game.Scripts.Battle.Services.HudFactories
{
    public class HudFactory : IHudFactory, IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly HudConfig _hudConfig;

        private StartPresenter _startPresenter;
        private UnitsPresenter _unitsPresenter;

        public HudFactory(IInstantiator instantiator, HudConfig hudConfig)
        {
            _instantiator = instantiator;
            _hudConfig = hudConfig;
        }

        public void CreateHUD()
        {
            var hud = _instantiator.InstantiatePrefabForComponent<BattleHud>(_hudConfig.HUD);

            _startPresenter = _instantiator.Instantiate<StartPresenter>(new object[] { hud.StartView });
            _startPresenter.Init();
            
            _unitsPresenter = _instantiator.Instantiate<UnitsPresenter>(new object[] { hud.UnitsView });
            _unitsPresenter.Init();
        }

        public void Dispose()
        {
            _startPresenter?.Dispose();
            _unitsPresenter?.Dispose();
        }
    }
}