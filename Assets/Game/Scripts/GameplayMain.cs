using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private WavesController _wavesController;
        private BuildingController _buildingController;
        private Castle _castle;
        private Registry<Enemy> _enemyRegistry;
        private Registry<Weapon> _weaponRegistry;
        private EndGamePanel _endGamePanel;
        private GameStatistics _gameStatistics;
        private CurrencyBank _currencyBank;

        [Inject]
        private void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Castle castle,
            Registry<Weapon> weaponRegistry,
            Registry<Enemy> enemyRegistry,
            EndGamePanel endGamePanel,
            GameStatistics gameStatistics,
            CurrencyBank currencyBank)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
            _enemyRegistry = enemyRegistry;
            _weaponRegistry = weaponRegistry;
            _endGamePanel = endGamePanel;
            _gameStatistics = gameStatistics;
            _currencyBank = currencyBank;
        }

        private void Start()
        {
            _wavesController.StartWaves();
            _buildingController.Init();
            _castle.Init();

            _castle.OnCastleHpEnded += OnCastleHpEndedHandler;
        }

        private void OnCastleHpEndedHandler()
        {
            StopEnemies();

            StopWeapons();

            StopBuilingController();

            OpenPanel();

            //SetMetaData();
        }

        private void OpenPanel()
        {
            _endGamePanel.Open(_gameStatistics.KilledEnemiesCount, _currencyBank.Total, 100);
        }

        private void StopBuilingController()
        {
            _buildingController.Stop();
        }

        private void StopWeapons()
        {
            foreach (var weapon in _weaponRegistry.All)
            {
                weapon.Stop();
            }
        }

        private void StopEnemies()
        {
            foreach (var enemy in _enemyRegistry.All)
            {
                enemy.Deactivate();
            }
        }
    }
}