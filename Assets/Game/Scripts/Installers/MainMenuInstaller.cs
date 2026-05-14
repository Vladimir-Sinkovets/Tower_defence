using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.MainMenuStatistics;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_metaCurrencyView")] [SerializeField] private MainMenuStatisticsView mainMenuStatisticsView;
        [SerializeField] private MainMenuView _mainMenuView;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();

            Container.BindInstance<IMainMenuStatisticsView>(mainMenuStatisticsView).AsSingle();
            Container.Bind<MainMenuStatisticsPresenter>().AsSingle().NonLazy();

            Container.BindInstance<IMainMenuView>(_mainMenuView).AsSingle();
            Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}