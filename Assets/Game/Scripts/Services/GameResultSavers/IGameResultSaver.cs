namespace Assets.Game.Scripts.Services.GameResultSavers
{
    public interface IGameResultSaver
    {
        void ApplySaveData(int earnedMetaCurrency, int wavesCount);
    }
}