using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingBuildingStateMachineData
    {
        public float SearchTargetInterval { get; set; }
        public Enemy CurrentTarget { get; set; }
        public ShootingBuildingFactory Config { get; set; }
        public Transform Transform { get; set; }
        public Transform WeaponRoot { get; set; }
        public Transform ProjectileStartPosition { get; set; }
        public WeaponAnimation PreShootAnimation { get; set; }
        public ShootingBuilding ShootingBuilding { get; set; }
    }
}