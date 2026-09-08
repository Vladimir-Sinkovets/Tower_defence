using UnityEngine;

namespace Assets.Game.Scripts.Arena.Buildings
{
    [CreateAssetMenu(fileName = "Building_config", menuName = "Arena/Building config")]
    public class BuildingConfig : ScriptableObject
    {
        public string ProjectilePrefabName;
        public ParticleSystem ShootVFXPrefab;
        public ParticleSystem HitVFXPrefab;
        public float AttackRadius = 4.0f;
        public float AttackInterval = 1.0f;
        public float ProjectileSpeed = 4.0f;
        public int Damage = 1;
        public float RotationSpeed = 360.0f;
        public float ArcHeight = 0.4f;
    }
}