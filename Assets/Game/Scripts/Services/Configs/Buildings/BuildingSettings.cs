using System;

namespace Assets.Game.Scripts.Services.Configs.Buildings
{
    [Serializable]
    public class BuildingSettings
    {
        public string Id;
        public int Price = 1;
        public float RadiusOfOccupiedSpace = 1.0f;
        public float AttackRadius = 4.0f;
        public float AttackInterval = 1.0f;
        public float ProjectileSpeed = 4.0f;
        public int Damage = 1;
        public float RotationSpeed = 360.0f;
        public float ArcHeight = 0.4f;
    }
}