using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI
{
    [CreateAssetMenu(fileName = "Arena_UI_config", menuName = "Arena/Arena UI config")]
    public class ArenaUIConfig : ScriptableObject
    {
        public ArenaHUD HUDPrefab;
        public ArenaInput InputPrefab;
    }
}