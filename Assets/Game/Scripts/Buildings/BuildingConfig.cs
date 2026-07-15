using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Building_config", menuName = "Configs/Building config")]
    public class BuildingConfig : ScriptableObject
    {
        public string Id;
        public Sprite Icon;
        public AssetReference ProjectilePrefab;
        public AssetReference ShootVFXPrefab;
        public AssetReference HitVFXPrefab;
        public AssetReference Prefab;
    }
}