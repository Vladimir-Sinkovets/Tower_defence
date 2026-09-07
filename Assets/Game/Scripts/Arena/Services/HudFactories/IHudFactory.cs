using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Arena.Services.HudFactories
{
    public interface IHudFactory
    {
        void CreateHUD(Health playerHealth);
    }
}