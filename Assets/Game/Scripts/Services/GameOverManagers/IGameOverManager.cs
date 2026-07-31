using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Services.GameOverManagers
{
    public interface IGameOverManager
    {
        void Init(Health castleHealth);
        void GameOver();
    }
}