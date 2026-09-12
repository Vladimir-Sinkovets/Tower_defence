using Assets.Game.Scripts.Arena.Services.EnemyDroppers;
using Assets.Game.Scripts.Arena.Services.GameStatistic;
using Photon.Pun;

namespace Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers
{
    public class EnemyDeathHandler : IEnemyDeathHandler
    {
        private readonly IGameStatistics _gameStatistics;
        private readonly IDropper _dropper;

        public EnemyDeathHandler(IGameStatistics gameStatistics, IDropper dropper)
        {
            _gameStatistics = gameStatistics;
            _dropper = dropper;
        }

        public void EnemyDiedHandler(ArenaEnemy arenaEnemy, int playerViewId)
        {
            if (PhotonNetwork.IsMasterClient)
                _dropper.Drop(arenaEnemy.transform.position, arenaEnemy.Config);
            
            var playerPhotonView = PhotonView.Find(playerViewId);
            
            if (playerPhotonView != null && playerPhotonView.IsMine)
            {
                _gameStatistics.IncreaseKilledEnemyCount();
            }
        }
    }
}