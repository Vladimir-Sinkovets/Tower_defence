namespace Assets.Game.Scripts.Battle.Services.BattleResultCalculators
{
    public class BattleResultCalculator : IBattleResultCalculator
    {
        public BattleResult GetGameOverResult() => new();
    }
}