using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ArenaEnemy : MonoBehaviour
    {
        [SerializeField] private ArenaEnemyConfig _enemyConfig;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private IPlayerAccessor _playerAccessor;
        
        private ArenaPlayer _target;

        [Inject]
        public void Construct(IPlayerAccessor playerAccessor) => _playerAccessor = playerAccessor;

        private void Awake()
        {
            _navMeshAgent.speed = _enemyConfig.Speed;
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            EnsureTarget();

            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (_target == null)
                return;
            
            _navMeshAgent.SetDestination(_target.Position);
        }

        private void EnsureTarget()
        {
            if (_target != null)
                return;

            _target = GetNearestTarget();
        }

        private ArenaPlayer GetNearestTarget()
        {
            var distance = float.MaxValue;

            ArenaPlayer nearestTarget = null;
            
            foreach (var player in _playerAccessor.Players)
            {
                if (Vector3.Distance(player.Position, transform.position) <= distance)
                {
                    nearestTarget = player;
                    
                    distance = Vector3.Distance(player.Position, transform.position);
                }
            }
            
            Debug.Log($"_playerAccessor.Players = {_playerAccessor.Players}");

            return nearestTarget;
        }
    }
}