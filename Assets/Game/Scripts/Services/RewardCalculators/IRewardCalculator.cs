using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.RewardCalculators
{
    public interface IRewardCalculator
    {
        UniTask<int> CalculateMetaCurrencyAsync(int wavesCount, int killedEnemiesCount);
    }
}