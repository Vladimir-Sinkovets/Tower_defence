namespace Assets.Game.Scripts.Services.Purchases
{
    public interface IInAppPurchaseExecutor
    {
        void Execute(string productId);
    }
}