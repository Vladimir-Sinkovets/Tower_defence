using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public interface IUpgradePanelView
    {
        event Action OnCloseButtonClicked;
        event Action OnOpenButtonClicked;
        event Action<string> OnUpgradeClicked;
        void ShowPanel();
        UniTask ClosePanel();
        void Init();
        void UpdateUpgradeList(IEnumerable<UpgradePanelViewModel> viewModels);
    }
}