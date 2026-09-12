using System;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    public class ExperienceService : IExperienceService
    {
        private readonly ArenaUpgradesConfig _config;
        
        public event Action OnExperienceChanged;
        
        public int Experience { get; private set; }
        public int ExperienceForNextLevel => _config.ExperienceForLevel;

        public ExperienceService(ArenaUpgradesConfig config) => _config = config;

        public void Increase(int amount)
        {
            Experience += amount;
            
            OnExperienceChanged?.Invoke();
        }
    }
}