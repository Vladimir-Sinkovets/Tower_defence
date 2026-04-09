using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private WavesController _wavesController;

        [Inject]
        private void Construct(WavesController waveController)
        {
            _wavesController = waveController;
        }

        private void Start()
        {
            _wavesController.StartWaves();
        }
    }
}