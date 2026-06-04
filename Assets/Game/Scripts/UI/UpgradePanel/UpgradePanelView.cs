using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Game.Scripts.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class UpgradePanelView : MonoBehaviour, IUpgradePanelView
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _container;
        [SerializeField] private UpgradeButton _upgradeButtonPrefab;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private PanelAppearanceAnimation _animation;
        
        private readonly List<UpgradeButton> _upgradeButtons = new List<UpgradeButton>();
        
        public event Action OnCloseButtonClicked;
        public event Action OnOpenButtonClicked;
        public event Action<string> OnUpgradeClicked;

        public void Init()
        {
            _openButton.onClick.AddListener(OnOpenButtonClickedHandler);
            _closeButton.onClick.AddListener(OnCloseButtonClickedHandler);
            
            _panel.SetActive(false);
        }

        public void UpdateUpgradeList(IEnumerable<UpgradePanelViewModel> viewModels)
        {
            ClearContainer();

            foreach (var viewModel in viewModels)
            {
                var upgradeButton = Instantiate(_upgradeButtonPrefab, _container);
                
                upgradeButton.Init(viewModel);

                upgradeButton.OnClicked += UpgradeClickedHandler;
                
                _upgradeButtons.Add(upgradeButton);
            }
        }

        public void ShowPanel()
        {
            _panel.SetActive(true);
            
            if (_animation != null)
                _animation.Show();
        }

        public async UniTask ClosePanel()
        {
            if (_animation != null)
                await _animation.Hide(CancellationToken.None);

            _panel.SetActive(false);
        }

        private void ClearContainer()
        {
            foreach(var upgradeButton in _upgradeButtons)
            {
                upgradeButton.OnClicked -= UpgradeClickedHandler;
                
                Destroy(upgradeButton.gameObject);
            }
            
            _upgradeButtons.Clear();
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveListener(OnOpenButtonClickedHandler);
            _closeButton.onClick.RemoveListener(OnCloseButtonClickedHandler);
        }

        private void UpgradeClickedHandler(string id) => OnUpgradeClicked?.Invoke(id);
        private void OnOpenButtonClickedHandler() => OnOpenButtonClicked?.Invoke();
        private void OnCloseButtonClickedHandler() => OnCloseButtonClicked?.Invoke();
    }
}