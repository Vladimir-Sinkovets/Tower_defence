using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
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
        private Registry<Building> _buildingRegistry;

        [Inject]
        private void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Castle castle,
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
            _enemyRegistry = enemyRegistry;
            _buildingRegistry = buildingRegistry;
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

            //StopWeapons();

            //OpenPanel();

            //SetMetaData();
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