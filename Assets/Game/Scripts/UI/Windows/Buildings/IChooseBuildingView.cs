using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Windows.Buildings
{
    public interface IChooseBuildingView
    {
        event Action<int> OnOptionChosen;
        event Action OnCloseButtonClicked;
        
        void ShowPanel();
        UniTask HidePanel(CancellationToken token = default);
        void Render(List<BuildingOptionViewModel> options);
        void HidePointer();
        void ShowPointer(Vector3 position);
    }

    public class BuildingOptionViewModel
    {
        public Sprite Icon;
        public int Index;
        public int Price;
        public bool IsAvailable;
    }
}