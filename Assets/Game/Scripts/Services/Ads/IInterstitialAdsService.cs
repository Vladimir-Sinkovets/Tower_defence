using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Ads
{
    public interface IInterstitialAdsService
    {
        UniTask ShowAdAsync(CancellationToken cancellationToken = default);
    }
}