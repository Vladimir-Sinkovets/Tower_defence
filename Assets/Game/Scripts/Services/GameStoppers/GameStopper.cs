using System.Linq;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Enemies.Interfaces;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Services.GameStoppers
{
    public class GameStopper : IGameStopper
    {
        private readonly Registry<Building> _buildingRegistry;
        private readonly Registry<Enemy> _enemyRegistry;
        private readonly IWavesController _waveController;
        private readonly PointSelector _pointSelector;

        public GameStopper(
            Registry<Building> buildingRegistry,
            Registry<Enemy> enemyRegistry,
            IWavesController waveController,
            PointSelector pointSelector)
        {
            _buildingRegistry = buildingRegistry;
            _enemyRegistry = enemyRegistry;
            _waveController = waveController;
            _pointSelector = pointSelector;
        }

        public void Stop()
        {
            StopBuildings();

            StopEnemies();
            
            StopWaves();

            DisableInput();
        }
        public void Resume()
        {
            ResumeBuildings();
            
            ResumeEnemies();
            
            ResumeWaves();
            
            EnableInput();
        }

        private void ResumeBuildings()
        {
            foreach (var building in _buildingRegistry.All.OfType<IStoppable>())
            {
                building.Resume();
            }
        }

        private void ResumeEnemies()
        {
            foreach (var enemy in _enemyRegistry.All)
            {
                if (!enemy.IsDead)
                    enemy.Activate();
            }
        }

        private void ResumeWaves() => _waveController.Resume();
        
        private void EnableInput() => _pointSelector.Enable();

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

        private void DisableInput() => _pointSelector.Disable();
    }
}