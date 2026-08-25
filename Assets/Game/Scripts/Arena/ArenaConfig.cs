using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    [CreateAssetMenu(fileName = "ArenaConfig", menuName = "Arena/ArenaConfig")]
    public class ArenaConfig : ScriptableObject
    {
        public float Speed = 5.0f;
        public string PlayerPrefabName = "Player";
    }
}