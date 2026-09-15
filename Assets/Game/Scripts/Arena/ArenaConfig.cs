using Assets.Game.Scripts.Arena.Buildings;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    [CreateAssetMenu(fileName = "ArenaConfig", menuName = "Arena/ArenaConfig")]
    public class ArenaConfig : ScriptableObject
    {
        public int Hp = 20;
        public float Speed = 5.0f;
        public int MetacurrencyPerKill = 2;
        public string PlayerPrefabName = "Player";
        public BuildingConfig BuildingConfig;
    }
}