namespace Assets.Game.Scripts.Services
{
    public class GameStatistics
    {
        public int KilledEnemiesCount { get; private set; }

        public void IncreaseKilledEnemyCount() => KilledEnemiesCount++;
    }
}