using System;
using Assets.Game.Scripts.Animations;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.ConnectMenu
{
    public class ConnectView : MonoBehaviour, IConnectMenuView
    {
        public event Action OnClosePanelButtonClicked;
        public event Action OnOpenPanelButtonClicked;
        public event Action OnConnectRoomButtonClicked;
        public event Action OnCreateRoomButtonClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private PanelAppearanceAnimation _panelAnimation;
        
        [SerializeField] private Button _openPanelButton;
        [SerializeField] private Button _closePanelButton;
        
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _connectRoomButton;
        
        [SerializeField] private TMP_InputField _roomIdInputField;

        private void Awake()
        {
            _openPanelButton.onClick.AddListener(OpenPanelButtonClickedHandler);
            _closePanelButton.onClick.AddListener(ClosePanelButtonClickedHandler);
            _createRoomButton.onClick.AddListener(CreateRoomButtonClickedHandler);
            _connectRoomButton.onClick.AddListener(ConnectRoomButtonClickedHandler);
        }
        
        public void Show()
        {
            _panel.SetActive(true);
            _panelAnimation.Show();
        }

        public async UniTask Hide()
        {
            await  _panelAnimation.Hide();
            _panel.SetActive(false);
        }

        private void ClosePanelButtonClickedHandler() => OnClosePanelButtonClicked?.Invoke();
        private void OpenPanelButtonClickedHandler() => OnOpenPanelButtonClicked?.Invoke();
        private void ConnectRoomButtonClickedHandler() => OnConnectRoomButtonClicked?.Invoke();
        private void CreateRoomButtonClickedHandler() => OnCreateRoomButtonClicked?.Invoke();

        private void OnDestroy()
        {
            _openPanelButton.onClick.RemoveListener(OpenPanelButtonClickedHandler);
            _closePanelButton.onClick.RemoveListener(ClosePanelButtonClickedHandler);
            _createRoomButton.onClick.RemoveListener(CreateRoomButtonClickedHandler);
            _connectRoomButton.onClick.RemoveListener(ConnectRoomButtonClickedHandler);
        }
    }
}