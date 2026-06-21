using System.Linq;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Services.GameStoppers
{
    public class GameStopper : IGameStopper
    {
        private readonly Registry<Building> _buildingRegistry;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IWavesController _waveController;

        public GameStopper(Registry<Building> buildingRegistry, Registry<Enemy> enemyRegistry, IWavesController waveController)
        {
            _buildingRegistry = buildingRegistry;
            _enemyRegistry = enemyRegistry;
            _waveController = waveController;
        }

        public void Stop()
        {
            StopBuildings();

            StopEnemies();
            
            StopWaves();
        }

        private void StopBuildings()
        {
            foreach (var building in _buildingRegistry.All.OfType<IStoppable>())
            {
                building.Stop();
            }
        }

        private void StopEnemies()
        {
            foreach (var enemy in _enemyRegistry.All)
            {
                enemy.Deactivate();
            }
        }

        private void StopWaves() => _waveController.Stop();
    }
}