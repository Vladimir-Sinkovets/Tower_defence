namespace Assets.Game.Scripts.Services.Analytics
{
    public interface IAnalytics
    {
        void GameStarted();
        void WaveStarted(int waveNumber, int enemiesToSpawn);
        void WaveCompleted(int waveNumber, int towersBuilt);
        void TowerBuilt(int coinsSpent, int towersTotal, int waveNumber);
        void BuildRejected();
        void CastleDamaged();
        void GameOver();
        void SessionRestarted();
        void ReturnedToMenu();
    }
}