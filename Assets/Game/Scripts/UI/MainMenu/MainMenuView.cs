using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public event Action OnStartClick;
        
        [SerializeField] private Button _startButton;

        private void Awake() => _startButton.onClick.AddListener(OnStartClickHandler);

        private void OnStartClickHandler() => OnStartClick?.Invoke();

        private void OnDestroy() => _startButton.onClick.RemoveListener(OnStartClickHandler);
    }
}