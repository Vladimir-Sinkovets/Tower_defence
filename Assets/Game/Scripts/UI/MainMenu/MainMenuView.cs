using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public event Action OnStartClick;
        public event Action OnCloseClick;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClickHandler);
            _closeButton.onClick.AddListener(OnCloseClickHandler);
        }

        private void OnStartClickHandler() => OnStartClick?.Invoke();
        private void OnCloseClickHandler() => OnCloseClick?.Invoke();

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClickHandler);
            _closeButton.onClick.RemoveListener(OnCloseClickHandler);
        }
    }
}