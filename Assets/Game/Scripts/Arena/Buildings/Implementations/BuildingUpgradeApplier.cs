using Assets.Game.Scripts.Arena.Buildings.Interfaces;

namespace Assets.Game.Scripts.Arena.Buildings.Implementations
{
    public class BuildingUpgradeApplier : IBuildingUpgradeApplier
    {
        public float ApplyBuildingAttackSpeedUpgrade(float attackInterval)
        {
            return attackInterval;
        }

        public int ApplyBuildingDamageUpgrade(int damage)
        {
            return damage;
        }
    }
}