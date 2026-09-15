using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Arena.Services.UpgradeServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI.Windows.Upgrades
{
    public class UpgradesView : MonoBehaviour, IUpgradesView
    {
        public event Action<Upgrade> OnUpgradeChosen;
        
        [SerializeField] private PanelAppearanceAnimation _panelAppearanceAnimation;
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _container;

        [SerializeField] private UpgradeButton _upgradeButtonPrefab;

        private readonly List<UpgradeButton> _buttons = new();

        public void UpdateUpgrades(IEnumerable<Upgrade> upgrades)
        {
            var upgradesList = upgrades.ToList();
            
            for (int i = 0; i < upgradesList.Count; i++)
            {
                if (i >= _buttons.Count)
                {
                    var button = Instantiate(_upgradeButtonPrefab, _container);
                    
                    _buttons.Add(button);

                    button.OnClick += OnClickHandler;
                }

                var upgrade = upgradesList[i];
                
                _buttons[i].SetUpgrade(upgrade);
            }
        }

        private void OnClickHandler(Upgrade obj) => OnUpgradeChosen?.Invoke(obj);

        public void ShowPanel()
        {
            _panel.SetActive(true);
            
            _panelAppearanceAnimation.Show();
        }
        
        public async UniTask HidePanel()
        {
            await _panelAppearanceAnimation.Hide();
            
            _panel.SetActive(false);
        }
    }
}