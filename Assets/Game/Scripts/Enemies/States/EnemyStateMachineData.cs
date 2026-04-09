using Assets.Game.Scripts.Enemies.Factories;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemies.States
{
    public class EnemyStateMachineData
    {
        public Transform Transform;
        public Health Target;
        public NavMeshAgent NavMeshAgent;
        public EnemyView View;
        public SimpleEnemyFactory Config;
        public SimpleEnemy Damageable;
    }
}