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
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private IPlayerAccessor _playerAccessor;
        
        private ArenaEnemyConfig _enemyConfig;
        
        private bool _inited;
        
        private ArenaPlayer _target;

        [Inject]
        public void Construct(IPlayerAccessor playerAccessor) => _playerAccessor = playerAccessor;

        public void Init(ArenaEnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
            
            _inited = true;
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            
            if (!_inited)
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
            
            Debug.Log($"distance = {distance}");

            return nearestTarget;
        }
    }
}