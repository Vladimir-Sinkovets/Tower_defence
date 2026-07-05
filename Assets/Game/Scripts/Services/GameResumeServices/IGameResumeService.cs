using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Services.GameResumeServices
{
    public interface IGameResumeService
    {
        void Init(Health castleHealth);
        void Resume();
    }
}