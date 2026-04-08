using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private Health _target;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        private void Start()
        {
            _stateMachine = new StateMachine();
            _data = new EnemyStateMachineData()
            {
                NavMeshAgent = _navMeshAgent,
                Target = _target,
                Transform = transform,
                View = _enemyView,
            };

            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyAttackState(_stateMachine, _data));

            _stateMachine.SetStartState<EnemyRunState>();
        }

        private void Update() => _stateMachine.Update();

        private void OnDestroy() => _stateMachine.Dispose();
    }
}