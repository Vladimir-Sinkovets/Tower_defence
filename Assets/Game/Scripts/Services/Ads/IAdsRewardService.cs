using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Ads
{
    public interface IAdsRewardService
    {
        UniTask ShowAdAsync(CancellationToken cancellationToken = default);
    }
}