using System;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;
using UnityEngine;
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
            _mainMenuView.OnCloseClick += OnCloseClickHandler;
        }

        private void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);
        private void OnCloseClickHandler() => CloseApplication();
        
        private void CloseApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Dispose() => _mainMenuView.OnStartClick -= OnStartClickHandler;
    }
}