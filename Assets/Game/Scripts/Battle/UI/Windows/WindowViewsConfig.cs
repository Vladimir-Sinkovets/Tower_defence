using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel
{
    [CreateAssetMenu(fileName = "Window_views_config", menuName = "Battle/Window views config")]
    public class WindowViewsConfig : ScriptableObject
    {
        public GameObject EndGameViewPrefab;
    }
}