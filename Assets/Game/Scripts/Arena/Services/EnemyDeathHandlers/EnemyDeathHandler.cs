using Assets.Game.Scripts.Arena.Services.GameStatistic;
using Photon.Pun;

namespace Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers
{
    public class EnemyDeathHandler : IEnemyDeathHandler
    {
        private readonly IGameStatistics _gameStatistics;

        public EnemyDeathHandler(IGameStatistics gameStatistics) => _gameStatistics = gameStatistics;

        public void EnemyDiedHandler(int playerViewId)
        {
            var playerPhotonView = PhotonView.Find(playerViewId);
            
            if (playerPhotonView.IsMine)
            {
                _gameStatistics.IncreaseKilledEnemyCount();
            }
        }
    }
}