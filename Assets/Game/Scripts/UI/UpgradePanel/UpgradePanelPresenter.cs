using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Upgrades;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public class UpgradePanelPresenter : IInitializable, IDisposable
    {
        private readonly UpgradeConfigs _configs;
        private readonly ISaveService _saveService;
        private readonly IUpgradePanelView _upgradePanelView;

        public UpgradePanelPresenter(UpgradeConfigs configs, ISaveService saveService, IUpgradePanelView upgradePanelView)
        {
            _configs = configs;
            _saveService = saveService;
            _upgradePanelView = upgradePanelView;

            _upgradePanelView.OnCloseButtonClicked += OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked += OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked += OnUpgradeClickedHandler;
        }

        public void Initialize() => _upgradePanelView.Init();

        private void Render()
        {
            var viewModels = _configs.List
                .Select(u => new UpgradePanelViewModel()
                {
                    Name = u.Name,
                    Level = GetLevel(u.Id),
                    Cost = u.GetCostByLevel(GetLevel(u.Id)),
                    Icon = u.Icon,
                    IsAvailable = IsAvailable(u.GetCostByLevel(GetLevel(u.Id))),
                    Upgrade = $"+{u.Upgrade}{u.Unit}",
                    Id = u.Id,
                });
            
            _upgradePanelView.UpdateUpgradeList(viewModels);
        }

        private bool IsAvailable(int getCostByLevel) => _saveService.GetSaveData().MetaCurrency >= getCostByLevel;

        private int GetLevel(string upgradeId)
        {
            var data = _saveService.GetSaveData();

            return data.Upgrades.GetValueOrDefault(upgradeId, 0);
        }

        private void OnUpgradeClickedHandler(string id)
        {
            var data = _saveService.GetSaveData();
            
            var upgrade = _configs.List.FirstOrDefault(u => u.Id == id);
            if (upgrade == null)
                return;
            
            var currentLevel = GetLevel(id);
            var cost = upgrade.GetCostByLevel(currentLevel);
            
            if (data.MetaCurrency < cost)
                return;
            
            data.MetaCurrency -= cost;

            if (!data.Upgrades.TryAdd(id, 1))
            {
                data.Upgrades[id] += 1;
            }

            _saveService.Save(data);
            
            Render();
        }
        
        private void OnOpenButtonClickedHandler()
        {
            Render();
            
            _upgradePanelView.ShowPanel();
        }
        
        private void OnCloseButtonClickedHandler() => _upgradePanelView.ClosePanel().Forget();
        
        public void Dispose()
        {
            _upgradePanelView.OnCloseButtonClicked -= OnCloseButtonClickedHandler;
            _upgradePanelView.OnOpenButtonClicked -= OnOpenButtonClickedHandler;
            _upgradePanelView.OnUpgradeClicked -= OnUpgradeClickedHandler;
        }
    }

    public class UpgradePanelViewModel
    {
        public string Name;
        public int Cost;
        public string Upgrade;
        public Sprite Icon;
        public bool IsAvailable;
        public int Level;
        public string Id;
    }
}