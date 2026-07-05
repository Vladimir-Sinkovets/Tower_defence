using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Ads
{
    public interface IAdsRewardService
    {
        UniTask LoadAdAsync(CancellationToken cancellationToken = default);
        UniTask ShowAdAsync(CancellationToken cancellationToken = default);
    }
}