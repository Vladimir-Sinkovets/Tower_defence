using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Battle.UI.StartPanel
{
    public class StartView : MonoBehaviour, IStartView
    {
        public event Action OnStartButtonClicked;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _panel;

        private void Awake() => _startButton.onClick.AddListener(OnStartButtonClickedHandler);
        
        public void Hide() => _panel.SetActive(false);

        private void OnStartButtonClickedHandler() => OnStartButtonClicked?.Invoke();

        private void OnDestroy() => _startButton.onClick.AddListener(OnStartButtonClickedHandler);
    }
}