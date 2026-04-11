using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class SceneLoader
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        public SceneLoader(ZenjectSceneLoader zenjectSceneLoader) => _zenjectSceneLoader = zenjectSceneLoader;

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) => _zenjectSceneLoader.LoadScene(sceneName, mode);

        public void ReloadCurrentScene()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;

            LoadScene(currentSceneName);
        }
    }
}