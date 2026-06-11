using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class UpgradeButton : MonoBehaviour
    {
        public event Action<string> OnClicked;
        
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject _redPanel;
        [SerializeField] private Button _applyButton;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _upgrade;
        
        private bool _isAvailable;
        private string _id;

        private void Awake() => _applyButton.onClick.AddListener(OnApplyButtonClickedHandler);

        public void Init(UpgradePanelViewModel viewModel)
        {
            _iconImage.sprite = viewModel.Icon;
            _name.text = viewModel.Name;
            _upgrade.text = viewModel.Upgrade;
            _cost.text = $"{viewModel.Cost}$";
            _level.text = $"lvl {viewModel.Level}";

            _isAvailable = viewModel.IsAvailable;
            _redPanel.SetActive(!viewModel.IsAvailable);

            _id = viewModel.Id;
        }

        private void OnApplyButtonClickedHandler()
        {
            if (_isAvailable)
                OnClicked?.Invoke(_id);
        }

        private void OnDestroy() => _applyButton.onClick.RemoveListener(OnApplyButtonClickedHandler);
    }
}