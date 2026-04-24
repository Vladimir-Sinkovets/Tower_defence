using System;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using Zenject;

namespace Assets.Game.Scripts.UI
{
    public class MainMenuPresenter : IDisposable
    {
        private SceneLoader _sceneLoader;
        private IMainMenuView _mainMenuView;

        [Inject]
        public void Construct(SceneLoader sceneLoader, IMainMenuView mainMenuView)
        {
            _sceneLoader = sceneLoader;
            _mainMenuView = mainMenuView;

            _mainMenuView.OnStartClick += OnStartClickHandler;
        }

        private void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);

        public void Dispose() => _mainMenuView.OnStartClick -= OnStartClickHandler;
    }
}