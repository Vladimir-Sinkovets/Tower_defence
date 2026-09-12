using Assets.Game.Scripts.Arena.Services.Experiences;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Experience : Drop
    {
        private IExperienceService _experienceService;
        
        private int _experience;

        [Inject]
        public void Construct(IExperienceService experienceService) => _experienceService = experienceService;

        public void Init(int experience)
        {
            _experience = experience;

            PlayAppearanceAnimation();
        }

        protected override void ApplyBonus() => _experienceService.Increase(_experience);
    }
}