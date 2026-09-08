namespace Assets.Game.Scripts.Arena.Services.GameStatistic
{
    public class GameStatistics : IGameStatistics
    {
        public int KilledEnemiesCount { get; private set; }

        public void IncreaseKilledEnemyCount() => KilledEnemiesCount++;
    }
}