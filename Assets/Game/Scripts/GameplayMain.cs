using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private WavesController _wavesController;
        private BuildingController _buildingController;

        [Inject]
        private void Construct(WavesController waveController, BuildingController buildingController)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
        }

        private void Start()
        {
            _wavesController.StartWaves();
            _buildingController.Init();
        }
    }
}