using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    [CreateAssetMenu(fileName = "Arena_upgrades_config", menuName = "Arena/Upgrades")]
    public class ArenaUpgradesConfig : ScriptableObject
    {
        public int ExperienceForLevel = 10;
    }
}