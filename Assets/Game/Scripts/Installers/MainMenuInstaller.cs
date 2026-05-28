using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.MainMenuStatistics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuStatisticsView _mainMenuStatisticsView;
        [SerializeField] private MainMenuView _mainMenuView;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();

            Container.BindInstance<IMainMenuStatisticsView>(_mainMenuStatisticsView).AsSingle();
            Container.Bind<MainMenuStatisticsPresenter>().AsSingle().NonLazy();

            Container.BindInstance<IMainMenuView>(_mainMenuView).AsSingle();
            Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}