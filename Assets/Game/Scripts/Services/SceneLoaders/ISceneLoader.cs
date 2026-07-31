using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Services.SceneLoaders
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        void ReloadCurrentScene();
    }
}