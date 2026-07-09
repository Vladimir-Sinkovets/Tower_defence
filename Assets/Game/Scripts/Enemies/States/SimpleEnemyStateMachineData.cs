using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemies.States
{
    public class SimpleEnemyStateMachineData
    {
        public Transform Transform;
        public Health TargetHealth;
        public Transform TargetTransform;
        public NavMeshAgent NavMeshAgent;
        public SimpleEnemyView View;
        public EnemyConfig Config;
        public Enemy Enemy;
    }
}