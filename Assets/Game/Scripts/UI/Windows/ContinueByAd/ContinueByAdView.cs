using System;
using Assets.Game.Scripts.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Windows.ContinueByAd
{
    public class ContinueByAdView : MonoBehaviour, IContinueByAdView
    {
        public event Action OnContinueButtonClicked;
        public event Action OnDeclineButtonClicked;
        
        [SerializeField] private RectTransform _panel;
        [SerializeField] private PanelAppearanceAnimation _animation;
        
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _declineButton;

        private void Awake()
        {
            _continueButton.onClick.AddListener(ContinueButtonHandler);
            _declineButton.onClick.AddListener(DeclineButtonHandler);
        }

        public void Open()
        {
            _panel.gameObject.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }

        public void Close()
        {
            if (_animation != null)
                _animation.Hide().Forget();

            _panel.gameObject.SetActive(false);
        }

        private void DeclineButtonHandler() => OnDeclineButtonClicked?.Invoke();

        private void ContinueButtonHandler() => OnContinueButtonClicked?.Invoke();

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(ContinueButtonHandler);
            _declineButton.onClick.RemoveListener(DeclineButtonHandler);
        }
    }
}