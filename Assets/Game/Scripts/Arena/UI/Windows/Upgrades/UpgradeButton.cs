using System;
using Assets.Game.Scripts.Arena.Services.UpgradeServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Arena.UI.Windows.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        public event Action<Upgrade> OnClick;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        
        private Upgrade _upgrade;

        private void Awake() => _button.onClick.AddListener(OnButtonClickedHandler);

        public void SetUpgrade(Upgrade upgrade)
        {
            _upgrade = upgrade;
            
            _image.sprite = upgrade.Icon;
            
            _text.text = $"{upgrade.Name} ({upgrade.Level})";
        }
        
        private void OnButtonClickedHandler() => OnClick?.Invoke(_upgrade);

        private void OnDestroy() => _button.onClick.RemoveListener(OnButtonClickedHandler);
    }
}