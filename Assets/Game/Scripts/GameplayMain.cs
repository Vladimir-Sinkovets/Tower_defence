using UnityEngine;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        [SerializeField] private EnemyWavesController _enemyWavesController;

        private void Start()
        {
            _enemyWavesController.StartWaves();
        }
    }
}