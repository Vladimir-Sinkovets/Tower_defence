using Assets.Game.Scripts.Animations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class EndGameView : MonoBehaviour, IEndGameView
    {
        public event Action OnRestartButtonClicked;
        public event Action OnMenuButtonClicked;

        [SerializeField] private RectTransform _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private TMP_Text _wavesCountText;
        [SerializeField] private TMP_Text _killsCountText;
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private TMP_Text _metaCurrencyText;

        [SerializeField] private PanelAppearanceAnimation _animation;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartButtonHandler);
            _menuButton.onClick.AddListener(MenuButtonHandler);
        }

        public void Open()
        {
            _panel.gameObject.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }
        
        public void ShowWavesCount(int wavesCount) => _wavesCountText.text = wavesCount.ToString();
        
        public void ShowKillsCount(int killsCount) => _killsCountText.text = killsCount.ToString();
        
        public void ShowCurrency(int currency) => _currencyText.text = currency.ToString();
        
        public void ShowEarnedMetaCurrency(int metaCurrency) => _metaCurrencyText.text = metaCurrency.ToString();
        
        private void RestartButtonHandler() => OnRestartButtonClicked?.Invoke();
        private void MenuButtonHandler() => OnMenuButtonClicked?.Invoke();

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartButtonHandler);
            _menuButton.onClick.RemoveListener(MenuButtonHandler);
        }
    }
}