using Assets.Game.Scripts.Animations;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Buildings.ShootingBuildings
{
    public class ShootingBuildingStateMachineData
    {
        public float SearchTargetInterval { get; set; }
        public ArenaEnemy CurrentTarget { get; set; }
        public BuildingConfig Config { get; set; }
        public Transform Transform { get; set; }
        public Transform WeaponRoot { get; set; }
        public Transform ProjectileStartPosition { get; set; }
        public WeaponAnimation PreShootAnimation { get; set; }
        public ShootingBuilding ShootingBuilding { get; set; }
        public int PlayerViewId { get; set; }
        public int Damage { get; set; }
        public float AttackInterval { get; set; }
    }
}