using Assets.Game.Scripts.Animations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        public event Action OnRestartButtonClicked;
        public event Action OnMenuButtonClicked;

        [SerializeField] private RectTransform _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private TMP_Text _wavesCountText;
        [SerializeField] private RectTransform _wavesCountPanel;

        [SerializeField] private TMP_Text _killedCountText;
        [SerializeField] private RectTransform _killedCountPanel;

        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private RectTransform _currencyPanel;

        [SerializeField] private TMP_Text _metaCurrencyText;
        [SerializeField] private RectTransform _metaCurrencyPanel;

        [SerializeField] private PanelAppearanceAnimation _animation;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartButtonHandler);
            _menuButton.onClick.AddListener(MenuButtonHandler);
        }

        public void Open(int wavesCount, int killedEnemyCount, int currencyCount, int metaCurrencyCount)
        {
            _wavesCountText.text = wavesCount.ToString();
            _killedCountText.text = killedEnemyCount.ToString();
            _currencyText.text = currencyCount.ToString();
            _metaCurrencyText.text = $"+{metaCurrencyCount}";

            _panel.gameObject.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }

        private void RestartButtonHandler() => OnRestartButtonClicked?.Invoke();
        private void MenuButtonHandler() => OnMenuButtonClicked?.Invoke();

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
        }
    }
}