namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyFactory
    {
        Enemy Create(EnemyConfig config);
    }
}