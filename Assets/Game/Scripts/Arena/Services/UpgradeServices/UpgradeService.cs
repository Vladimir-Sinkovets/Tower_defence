using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Services.Experiences;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.UpgradeServices
{
    public class UpgradeService : IUpgradeService, IInitializable, IDisposable
    {
        public event Action OnLevelUp;
        
        private readonly IExperienceService _experienceService;
        private readonly ArenaUpgradesConfig _config;
        
        private List<Upgrade> _upgrades;

        public UpgradeService(IExperienceService experienceService, ArenaUpgradesConfig config)
        {
            _experienceService = experienceService;
            _config = config;
        }

        public void Initialize()
        {
            _upgrades = new List<Upgrade>();

            foreach (var upgrade in _config.Upgrades)
            {
                _upgrades.Add(new Upgrade()
                {
                    Name = upgrade.Name,
                    Icon = upgrade.Icon,
                    Level = 0,
                });
            }

            _experienceService.OnExperienceChanged += OnExperienceChangedHandler;
        }

        private void OnExperienceChangedHandler()
        {
            if (HasExperienceForNextLevel())
                OnLevelUp?.Invoke();
        }

        public IEnumerable<Upgrade> GetUpgrades() => _upgrades;

        public void BuyUpgrade(Upgrade upgrade)
        {
            if (_experienceService.Experience < _config.ExperienceForLevel)
                return;
            
            _experienceService.Decrease(_config.ExperienceForLevel);
            
            if (_upgrades.Contains(upgrade)) 
                upgrade.Level++;
        }

        public bool HasExperienceForNextLevel() => _config.ExperienceForLevel <= _experienceService.Experience;

        public void Dispose() => _experienceService.OnExperienceChanged -= OnExperienceChangedHandler;
    }
}