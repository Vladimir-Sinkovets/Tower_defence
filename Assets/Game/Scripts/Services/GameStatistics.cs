namespace Assets.Game.Scripts.Services
{
    public class GameStatistics
    {
        private int _killedEnemiesCount;

        public GameStatistics(EnemyEvents enemyEvents)
        {
            enemyEvents.OnEnemyDied += OnEnemyDiedHandler;
        }

        public int KilledEnemiesCount { get => _killedEnemiesCount; }

        private void OnEnemyDiedHandler() => _killedEnemiesCount++;
    }
}