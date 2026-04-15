namespace Assets.Game.Scripts.Services
{
    public class GameStatistics
    {
        private int _killedEnemiesCount;

        public int KilledEnemiesCount { get => _killedEnemiesCount; }

        public void AddKilledEnemy() => _killedEnemiesCount++;
    }
}