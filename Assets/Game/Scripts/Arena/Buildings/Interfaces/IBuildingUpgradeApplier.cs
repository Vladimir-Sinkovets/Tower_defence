namespace Assets.Game.Scripts.Arena.Buildings.Interfaces
{
    public interface IBuildingUpgradeApplier
    {
        float ApplyBuildingAttackSpeedUpgrade(float attackInterval);
        int ApplyBuildingDamageUpgrade(int damage);
    }
}