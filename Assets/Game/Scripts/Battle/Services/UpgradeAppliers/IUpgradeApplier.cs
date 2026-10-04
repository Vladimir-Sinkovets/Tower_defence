namespace Assets.Game.Scripts.Battle.Services.UpgradeAppliers
{
    public interface IUpgradeApplier
    {
        int ApplyTankDamageUpgrade(int baseDamage);
        int ApplyStartCurrencyUpgrade(int baseCurrency);
        int ApplyDefaultUnitDamageUpgrade(int baseDamage);
    }
}