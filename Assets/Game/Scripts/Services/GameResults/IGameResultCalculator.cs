namespace Assets.Game.Scripts.Services.GameResults
{
    public interface IGameResultCalculator
    {
        GameOverResult Calculate();
        GameOverResult GameOverResult { get; }
    }
}