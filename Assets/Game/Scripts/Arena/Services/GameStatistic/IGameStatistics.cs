namespace Assets.Game.Scripts.Arena.Services.GameStatistic
{
    public interface IGameStatistics
    {
        int KilledEnemiesCount { get; }
        void IncreaseKilledEnemyCount();
    }
}