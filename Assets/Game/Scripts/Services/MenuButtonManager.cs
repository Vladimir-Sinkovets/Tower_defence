using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class MenuButtonManager : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            _startButton.onClick.AddListener(OnStartClickHandler);
        }

        private void OnStartClickHandler() => _sceneLoader.LoadScene(SceneNames.Game);

        private void OnDestroy() => _startButton.onClick.RemoveAllListeners();
    }
}