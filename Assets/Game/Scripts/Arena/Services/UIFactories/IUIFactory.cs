using Assets.Game.Scripts.Arena.UI;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.Services.UIFactories
{
    public interface IUIFactory
    {
        void CreateHUD(Health playerHealth);
        ArenaInput CreateArenaInput();
    }
}