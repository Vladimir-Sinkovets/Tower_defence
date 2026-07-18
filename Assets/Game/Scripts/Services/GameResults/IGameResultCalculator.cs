using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.GameResults
{
    public interface IGameResultCalculator
    {
        UniTask<GameOverResult> CalculateAsync();
        GameOverResult GameOverResult { get; }
    }
}