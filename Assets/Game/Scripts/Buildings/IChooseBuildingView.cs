using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Buildings;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public interface IChooseBuildingView
    {
        event Action<Vector3> OnClicked;
        event Action<int> OnOptionChosen;
        event Action OnCloseButtonClicked;
        
        void Show();
        void Hide();
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