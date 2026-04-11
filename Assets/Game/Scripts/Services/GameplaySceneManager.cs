using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class GameplaySceneManager : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void OnMenuClickHandler() => _sceneLoader.LoadScene(SceneNames.Menu);
        public void OnRestartClickHandler() => _sceneLoader.ReloadCurrentScene();
    }
}