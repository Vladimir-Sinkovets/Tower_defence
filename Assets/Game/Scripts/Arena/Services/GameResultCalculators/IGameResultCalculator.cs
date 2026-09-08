namespace Assets.Game.Scripts.Arena.Services.GameResultCalculators
{
    public interface IGameResultCalculator
    {
        GameOverResult GameOverResult { get; }
        GameOverResult Calculate();
    }
}