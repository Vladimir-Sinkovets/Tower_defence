namespace Assets.Game.Scripts.Services
{
    public class GameStatictics
    {
        private int _killedEnemiesCount;

        public GameStatictics(EnemyEvents enemyEvents)
        {
            enemyEvents.OnEnemyDied += OnEnemyDiedHandler;
        }

        public int KilledEnemiesCount { get => _killedEnemiesCount; }

        private void OnEnemyDiedHandler() => _killedEnemiesCount++;
    }
}