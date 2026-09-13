using System;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    public class ExperienceService : IExperienceService
    {
        public event Action OnExperienceChanged;
        
        public int Experience { get; private set; }

        public void Increase(int amount)
        {
            Experience += amount;
            
            OnExperienceChanged?.Invoke();
        }

        public void Decrease(int amount)
        {
            Experience -= amount;
            
            OnExperienceChanged?.Invoke();
        }
    }
}