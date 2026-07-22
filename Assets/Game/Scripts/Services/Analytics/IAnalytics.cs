namespace Assets.Game.Scripts.Services.Analytics
{
    public interface IAnalytics
    {
        void GameStarted();
        void WaveStarted(int waveNumber, int enemiesToSpawn);
        void WaveCompleted(int waveNumber);
        void TowerBuilt(int coinsSpent, int waveNumber);
        void BuildRejected();
        void CastleDamaged();
        void GameOver();
        void SessionRestarted();
        void ReturnedToMenu();
    }
}