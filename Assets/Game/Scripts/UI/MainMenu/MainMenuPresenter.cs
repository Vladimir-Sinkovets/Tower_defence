using System;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;
using Zenject;

namespace Assets.Game.Scripts.UI
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IMainMenuView _mainMenuView;

        public MainMenuPresenter(ISceneLoader sceneLoader, IMainMenuView mainMenuView)
        {
            _sceneLoader = sceneLoader;
            _mainMenuView = mainMenuView;
        }
        
        public void Initialize()
        {
            _mainMenuView.OnStartClick += OnStartClickHandler;
        }

        private void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);

        public void Dispose() => _mainMenuView.OnStartClick -= OnStartClickHandler;
    }
}