using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.MainMenuStatistics;
using Assets.Game.Scripts.UI.UpgradePanel;
using Assets.Game.Scripts.Upgrades.Implementations;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuStatisticsView _mainMenuStatisticsView;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private UpgradePanelView _upgradePanelView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UpgradeService>().AsSingle();
            
            Container.BindInstance<IMainMenuStatisticsView>(_mainMenuStatisticsView).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuStatisticsPresenter>().AsSingle();

            Container.BindInstance<IMainMenuView>(_mainMenuView).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();

            Container.BindInstance<IUpgradePanelView>(_upgradePanelView).AsSingle();
            Container.BindInterfacesAndSelfTo<UpgradePanelPresenter>().AsSingle();
        }
    }
}