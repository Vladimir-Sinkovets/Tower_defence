using System;
using System.Threading;
using Assets.Game.Scripts.Animations;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Arena.UI.Windows.EndGame
{
    public class EndGameView : MonoBehaviour, IEndGameView
    {
        public event Action OnRestartButtonClicked;
        public event Action OnMenuButtonClicked;

        [SerializeField] private RectTransform _panel;
        [SerializeField] private Button _menuButton;

        [SerializeField] private TMP_Text _killsCountText;
        [SerializeField] private TMP_Text _metaCurrencyText;

        [SerializeField] private PanelAppearanceAnimation _animation;

        private void Awake()
        {
            _menuButton.onClick.AddListener(MenuButtonHandler);
        }

        public void Open()
        {
            _panel.gameObject.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }

        public async UniTask Close(CancellationToken token = default)
        {
            if (_animation != null)
                await _animation.Hide(token);

            _panel.gameObject.SetActive(false);
        }
        
        public void ShowKillsCount(int killsCount) => _killsCountText.text = killsCount.ToString();
        
        public void ShowEarnedMetaCurrency(int metaCurrency) => _metaCurrencyText.text = metaCurrency.ToString();

        private void MenuButtonHandler() => OnMenuButtonClicked?.Invoke();

        private void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(MenuButtonHandler);
        }
    }
}