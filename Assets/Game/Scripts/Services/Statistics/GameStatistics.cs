namespace Assets.Game.Scripts.Services.Statistics
{
    public class GameStatistics
    {
        public int KilledEnemiesCount { get; private set; }

        public void IncreaseKilledEnemyCount() => KilledEnemiesCount++;
    }
}