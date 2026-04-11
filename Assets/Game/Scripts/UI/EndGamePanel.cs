using System;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        public event Action OnRestartButtonClicked;
        public event Action OnMenuButtonClicked;

        [SerializeField] private RectTransform _panel;

        [SerializeField] private TMP_Text _wavesCountText;
        [SerializeField] private RectTransform _wavesCountPanel;

        [SerializeField] private TMP_Text _killedCountText;
        [SerializeField] private RectTransform _killedCountPanel;

        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private RectTransform _currencyPanel;

        [SerializeField] private TMP_Text _metaCurrencyText;
        [SerializeField] private RectTransform _metaCurrencyPanel;

        public void Open(int wavesCount, int killedEnemyCount, int currencyCount, int metaCurrencyCount)
        {
            _wavesCountText.text = wavesCount.ToString();
            _killedCountText.text = killedEnemyCount.ToString();
            _currencyText.text = currencyCount.ToString();
            _metaCurrencyText.text = $"+{metaCurrencyCount}";

            _panel.gameObject.SetActive(true);
        }

        public void RestartButtonHandler() => OnRestartButtonClicked?.Invoke();
        public void MenuButtonHandler() => OnMenuButtonClicked?.Invoke();
    }
}