namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public interface IGameResultSaver
    {
        void ApplyMetaCurrency(int earnedMetaCurrency);
        void ApplyWavesRecord(int wavesCount);
    }
}