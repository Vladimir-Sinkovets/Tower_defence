using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Buildings;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public interface IChooseBuildingView
    {
        event Action<BuildingConfig> OnOptionChosen;
        event Action OnCloseButtonClicked;
        event Action<Vector3> OnClicked;
        
        void Show();
        void Hide();
        void Render(List<BuildingOptionViewModel> options);
        void HidePointer();
        void ShowPointer(Vector3 position);
    }

    public class BuildingOptionViewModel
    {
        public BuildingConfig Config;
        public bool IsAvailable;
    }
}