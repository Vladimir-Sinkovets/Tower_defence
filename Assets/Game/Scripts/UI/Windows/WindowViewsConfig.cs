using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.UI.Windows
{
    [CreateAssetMenu(fileName = "WindowViewsConfig", menuName = "Configs/Window views config")]
    public class WindowViewsConfig : ScriptableObject
    {
        public AssetReference ChooseBuildingViewPrefab;
        public AssetReference EndGameViewPrefab;
        public AssetReference ContinueByAdViewPrefab;
    }
}