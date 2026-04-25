using System;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.UI
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IMainMenuView _mainMenuView;

        public MainMenuPresenter(SceneLoader sceneLoader, IMainMenuView mainMenuView)
        {
            _sceneLoader = sceneLoader;
            _mainMenuView = mainMenuView;

            _mainMenuView.OnStartClick += OnStartClickHandler;
        }

        private void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);

        public void Dispose() => _mainMenuView.OnStartClick -= OnStartClickHandler;
    }
}