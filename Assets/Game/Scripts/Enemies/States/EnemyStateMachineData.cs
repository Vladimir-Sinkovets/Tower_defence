using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyStateMachineData
    {
        public Transform Transform;
        public Health TargetHealth;
        public Transform TargetTransform;
        public NavMeshAgent NavMeshAgent;
        public EnemyView View;
        public EnemyConfig Config;
        public SimpleEnemy Enemy;
    }
}