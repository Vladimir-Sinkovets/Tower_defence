using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Services.Experiences;
using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.UpgradeServices
{
    public class UpgradeService : IUpgradeService, IInitializable, IDisposable
    {
        public event Action OnLevelUp;
        
        private readonly IExperienceService _experienceService;
        private readonly ArenaUpgradesConfig _config;
        private readonly IPlayerAccessor _playerAccessor;
        private readonly IPlayerController _playerController;

        private List<Upgrade> _upgrades;

        public UpgradeService(
            IExperienceService experienceService,
            ArenaUpgradesConfig config,
            IPlayerAccessor playerAccessor,
            IPlayerController playerController)
        {
            _experienceService = experienceService;
            _config = config;
            _playerAccessor = playerAccessor;
            _playerController = playerController;
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
                    Type = upgrade.Type,
                    EachLevelCoefficient = upgrade.EachLevelCoefficient,
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

            if (!_upgrades.Contains(upgrade))
                return;
            
            _experienceService.Decrease(_config.ExperienceForLevel);
            
            upgrade.Level++;
            
            ApplyBonus(upgrade);
        }

        public bool HasExperienceForNextLevel() => _config.ExperienceForLevel <= _experienceService.Experience;

        private void ApplyBonus(Upgrade upgrade)
        {
            switch (upgrade.Type)
            {
                case UpgradeType.AttackSpeed:
                    _playerAccessor.CurrentPlayer.ShootingBuilding.IncreaseAttackSpeed(
                        (float) Math.Pow(
                            Mathf.Clamp01(upgrade.EachLevelCoefficient),
                            upgrade.Level));
                    break;
                case UpgradeType.Damage:
                    _playerAccessor.CurrentPlayer.ShootingBuilding.IncreaseDamage(
                        (int)(upgrade.Level * upgrade.EachLevelCoefficient));
                    break;
                case UpgradeType.Hp:
                    _playerAccessor.CurrentPlayer.Health.IncreaseHp(
                        (int)(upgrade.Level * upgrade.EachLevelCoefficient));
                    break;
                case UpgradeType.MovementSpeed:
                    _playerController.IncreaseMovementSpeed(
                        upgrade.Level * upgrade.EachLevelCoefficient);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose() => _experienceService.OnExperienceChanged -= OnExperienceChangedHandler;
    }
}