using System;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    public interface IExperienceService
    {
        event Action OnExperienceChanged;
        int Experience { get; }
        int ExperienceForNextLevel { get; }
        void Increase(int amount);
    }
}