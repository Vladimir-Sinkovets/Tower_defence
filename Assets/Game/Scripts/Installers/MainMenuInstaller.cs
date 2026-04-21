using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MetaCurrencyView _metaCurrencyView;
        [SerializeField] private MainMenuView _mainMenuView;
        
        public override void InstallBindings()
        {
            Container.Bind<MetaCurrencyService>().AsSingle();

            Container.Bind<SceneLoader>().AsSingle();

            Container.BindInstance<IMetaCurrencyView>(_metaCurrencyView).AsSingle();

            Container.Bind<MetaCurrencyPresenter>().AsSingle().NonLazy();

            Container.BindInstance<IMainMenuView>(_mainMenuView).AsSingle();

            Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}