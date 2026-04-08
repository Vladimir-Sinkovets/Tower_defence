using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemy
{
    public class EnemyStateMachineData
    {
        public Transform Transform;
        public Transform Target;
        public NavMeshAgent NavMeshAgent;
        public EnemyView View;
    }
}