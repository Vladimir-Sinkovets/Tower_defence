using Assets.Game.Scripts.UI.Windows.Buildings;
using Assets.Game.Scripts.UI.Windows.EndGame;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Windows
{
    [CreateAssetMenu(fileName = "WindowViewsConfig", menuName = "Configs/Window views config")]
    public class WindowViewsConfig : ScriptableObject
    {
        public ChooseBuildingView ChooseBuildingViewPrefab;
        public EndGameView EndGameViewPrefab;
    }
}