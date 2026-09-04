using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemySpawners
{
    [CreateAssetMenu(fileName = "ArenaEnemyConfig", menuName = "Arena/Arena enemy config")]
    public class ArenaEnemyConfig : ScriptableObject
    {
        public string PrefabName = "Enemy";
        public int Hp = 10;
        public float Speed = 5.5f;
    }
}