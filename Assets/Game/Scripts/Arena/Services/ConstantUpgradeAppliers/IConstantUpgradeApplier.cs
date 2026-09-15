namespace Assets.Game.Scripts.Arena.Services.ConstantUpgradeAppliers
{
    public interface IConstantUpgradeApplier
    {
        int ApplyDamageUpgrade(int baseDamage);
        int ApplyHpUpgrade(int baseHp);
    }
}