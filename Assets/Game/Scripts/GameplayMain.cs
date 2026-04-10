using Assets.Game.Scripts.Buildings;
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

        [Inject]
        private void Construct(WavesController waveController, BuildingController buildingController, Castle castle)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
        }

        private void Start()
        {
            _wavesController.StartWaves();
            _buildingController.Init();
            _castle.Init();
        }
    }
}