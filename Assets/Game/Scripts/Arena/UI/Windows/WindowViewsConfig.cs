using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI.Windows
{
    [CreateAssetMenu(fileName = "WindowViewsConfig", menuName = "Arena/Window views config")]
    public class WindowViewsConfig : ScriptableObject
    {
        public GameObject EndGameViewPrefab;
    }
}