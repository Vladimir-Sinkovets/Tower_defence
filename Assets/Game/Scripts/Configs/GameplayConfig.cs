using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay Config")]
    public class GameplayConfig : ScriptableObject
    {
        public int ContinuesAfterDeath = 1;
    }
}