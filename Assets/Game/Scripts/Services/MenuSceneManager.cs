using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class MenuSceneManager : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);
    }
}