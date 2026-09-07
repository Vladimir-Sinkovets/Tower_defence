using Assets.Game.Scripts.Arena.UI;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    [CreateAssetMenu(fileName = "ArenaConfig", menuName = "Arena/ArenaConfig")]
    public class ArenaConfig : ScriptableObject
    {
        public int Hp = 20;
        public float Speed = 5.0f;
        public string PlayerPrefabName = "Player";
        public ArenaHUD HUDPrefab;
    }
}