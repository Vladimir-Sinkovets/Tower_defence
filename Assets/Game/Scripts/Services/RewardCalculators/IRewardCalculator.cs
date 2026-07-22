namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public interface IRewardCalculator
    {
        int CalculateMetaCurrency(int wavesCount, int killedEnemiesCount);
    }
}