using Assets.Game.Scripts.Buildings;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Upgrades.Interfaces
{
    public interface IBuildingUpgradeApplier
    {
        UniTask<int> ApplyBuildingDamageUpgradeAsync(int baseDamage, BuildingType buildingType);
        UniTask<float> ApplyBuildingAttackSpeedUpgradeAsync(float baseInterval, BuildingType buildingType);
        UniTask<int> ApplyCastleHpUpgradeAsync(int baseHp);
    }
}