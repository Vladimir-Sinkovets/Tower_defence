using Assets.Game.Scripts.Buildings;

namespace Assets.Game.Scripts.Upgrades.Interfaces
{
    public interface IBuildingUpgradeApplier
    {
        int ApplyBuildingDamageUpgrade(int baseDamage, BuildingType buildingType);
        float ApplyBuildingAttackSpeedUpgrade(float baseInterval, BuildingType buildingType);
        int ApplyCastleHpUpgrade(int baseHp);
    }
}