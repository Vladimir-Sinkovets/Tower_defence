using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Building_config", menuName = "Configs/Building config")]
    public class BuildingConfig : ScriptableObject
    {
        public float RadiusOfOccupiedSpace = 1.0f;
        public float AttackRadius = 4.0f;
        public float AttackInterval = 1.0f;
        public float ProjectileSpeed = 4.0f;
        public int Damage = 1;
        public float RotationSpeed = 360.0f;
        public float ArcHeight = 0.4f;
        public AssetReference ProjectilePrefab;
        public AssetReference ShootVFXPrefab;
        public AssetReference HitVFXPrefab;
        public AssetReference Prefab;
    }
}