using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.UpgradePanel
{
    public interface IArenaUpgradePanelView
    {
        event Action OnCloseButtonClicked;
        event Action OnOpenButtonClicked;
        event Action<string> OnUpgradeClicked;
        void ShowPanel();
        UniTask ClosePanel();
        void Init();
        void UpdateUpgradeList(List<ArenaUpgradePanelViewModel> viewModels);
    }
}