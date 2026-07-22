using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class StartGameEntryPoint : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private GameSettingsService _gameSettingsService;

        [Inject]
        private void Construct(SceneLoader sceneLoader, GameSettingsService gameSettingsService)
        {
            _sceneLoader = sceneLoader;
            _gameSettingsService = gameSettingsService;
        }
        
        private void Start() => LoadAsync().Forget();

        private async UniTask LoadAsync()
        {
            await _gameSettingsService.FetchRemoteConfigAsync();
            
            _sceneLoader.LoadScene(SceneNames.Menu);
        }
    }
}