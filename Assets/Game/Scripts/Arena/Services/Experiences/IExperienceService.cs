using System;

namespace Assets.Game.Scripts.Arena.Services.Experiences
{
    public interface IExperienceService
    {
        event Action OnExperienceChanged;
        int Experience { get; }
        void Increase(int amount);
        void Decrease(int amount);
    }
}