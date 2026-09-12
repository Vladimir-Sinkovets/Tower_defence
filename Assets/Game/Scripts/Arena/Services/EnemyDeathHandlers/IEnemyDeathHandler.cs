namespace Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers
{
    public interface IEnemyDeathHandler
    {
        void EnemyDiedHandler(ArenaEnemy arenaEnemy, int playerViewId);
    }
}