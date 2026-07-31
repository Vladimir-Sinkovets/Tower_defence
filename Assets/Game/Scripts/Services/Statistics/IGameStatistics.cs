namespace Assets.Game.Scripts.Services.Statistics
{
    public interface IGameStatistics
    {
        int KilledEnemiesCount { get; }
        void IncreaseKilledEnemyCount();
    }
}