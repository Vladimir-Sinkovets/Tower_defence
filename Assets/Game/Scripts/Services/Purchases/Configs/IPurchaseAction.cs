using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Services.Purchases.Configs
{
    public interface IPurchaseAction
    {
        void Execute(ISaveService saveService);
    }
}