using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class BuildingWeapon : MonoBehaviour
    {
        [SerializeField] private float _attackRadius = 4.0f;
        [SerializeField] private float _attackInterval = 1.0f;
        [SerializeField] private float _projectileSpeed = 4.0f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileStartPosition;


        private GameContext _context;
        private Enemy _currenttarget;

        private float _nextShootTime = 0;

        [Inject]
        private void Construct(GameContext context)
        {
            _context = context;
        }

        private void Update()
        {
            if (_currenttarget != null)
                Attack();
            else
                FindTarget();
        }

        private void Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            _nextShootTime = Time.time + _attackInterval;

            var projectile = Instantiate(_projectilePrefab);

            projectile.transform.position = _projectileStartPosition.transform.position;

            projectile.Init(_currenttarget.Health, _damage, _projectileSpeed);
        }

        private void FindTarget()
        {
            foreach (var enemy in _context.All)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) <= _attackRadius)
                {
                    _currenttarget = enemy;

                    break;
                }
            }
        }
    }
}