using System;
using Assets.Game.Scripts.Arena.Services.Experiences;

namespace Assets.Game.Scripts.Arena.UI.Experience
{
    public class ExperiencePresenter : IDisposable
    {
        private readonly IExperienceService _service;
        private readonly ArenaUpgradesConfig _upgradeConfig;
        private readonly IExperienceView _view;

        public ExperiencePresenter(IExperienceService service, ArenaUpgradesConfig upgradeConfig, IExperienceView view)
        {
            _service = service;
            _upgradeConfig = upgradeConfig;
            _view = view;
        }
        
        public void Init()
        {
            _service.OnExperienceChanged += OnExperienceChangedHandler;
            
            _view.SetExperienceBar(0);
        }

        private void OnExperienceChangedHandler() =>
            _view.SetExperienceBar(_service.Experience / (float)_upgradeConfig.ExperienceForLevel);

        public void Dispose() => _service.OnExperienceChanged -= OnExperienceChangedHandler;
    }
}