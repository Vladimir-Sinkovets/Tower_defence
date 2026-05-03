using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingBuilding : Building
    {
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _preShootAnimation;
        [SerializeField] private float _searchTargetInterval = 0.2f;

        private Registry<Enemy> _enemiesRegistry;

        private ShootingBuildingFactory _config;

        private Enemy _currentTarget;

        private float _nextShootTime;
        private float _nextSearchTime;

        private CancellationTokenSource _shootCts;
        
        private bool _isStopped;

        [Inject]
        public void Construct(Registry<Enemy> enemyRegistry, Registry<Building> buildingRegistry)
        {
            base.Construct(buildingRegistry);

            _enemiesRegistry = enemyRegistry;
        }
        

        private void Update()
        {
            if (_isStopped)
                return;

            if (_currentTarget != null)
            {
                if (Vector3.Distance(_currentTarget.transform.position, transform.position) > _config.AttackRadius)
                {
                    ClearTarget();
                    return;
                }

                RotateWeapon();

                Attack();
            }
            else if (Time.time >= _nextSearchTime)
            {
                _nextSearchTime = Time.time + _searchTargetInterval;

                FindTarget();
            }
        }
        

        public void Init(ShootingBuildingFactory config)
        {
            base.Init(config);

            _isStopped = false;

            _config = config;

            ClearTarget();

            _nextShootTime = 0f;

            _nextSearchTime = 0f;
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = new CancellationTokenSource();
        }

        public override void Stop()
        {
            _isStopped = true;
            
            _shootCts?.Cancel();

            StopAllCoroutines();

            ClearTarget();
        }

        
        private void Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            _nextShootTime = Time.time + _config.AttackInterval;

            Shoot(_shootCts.Token).Forget();
        }

        private async UniTask Shoot(CancellationToken ct)
        {
            if (_preShootAnimation != null)
                await _preShootAnimation.PlayBeforeAttackAnimation(ct);

            if (_config.ShootVFX != null)
            {
                var vfx = Instantiate(_config.ShootVFX, _projectileStartPosition.position, Quaternion.identity);

                Destroy(vfx.gameObject, vfx.main.duration);
            }

            var projectile = Instantiate(_config.ProjectilePrefab);

            projectile.transform.position = _projectileStartPosition.transform.position;

            projectile.Init(_currentTarget, _config.Damage, _config.ProjectileSpeed, _config.ArcHeight, _config.HitVFX);
        }

        private void FindTarget()
        {
            if (_enemiesRegistry.All == null)
                return;

            ClearTarget();

            var minDistance = float.MaxValue;
            Enemy nearestEnemy = null;

            foreach (var enemy in _enemiesRegistry.All)
            {
                if (enemy.Health.IsDead)
                    continue;

                var distance = Vector3.Distance(enemy.transform.position, transform.position);

                if (distance <= _config.AttackRadius && minDistance > distance)
                {
                    minDistance = distance;

                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy == null)
                return;

            _currentTarget = nearestEnemy;

            _currentTarget.Health.OnDied += OnCurrentTargetDiedHandler;

            _nextShootTime = Time.time + _config.AttackInterval;
        }

        private void RotateWeapon()
        {
            if (_currentTarget == null) return;

            var direction = _currentTarget.transform.position - _weaponRoot.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                var targetRotation = Quaternion.LookRotation(direction);
                _weaponRoot.rotation = Quaternion.RotateTowards(
                    _weaponRoot.rotation,
                    targetRotation,
                    _config.RotationSpeed * Time.deltaTime
                );
            }
        }

        private void OnCurrentTargetDiedHandler() => ClearTarget();

        
        protected override void OnDestroy()
        {
            base.OnDestroy();

            StopAllCoroutines();

            ClearTarget();
            
            _shootCts?.Cancel();
            _shootCts?.Dispose();
            _shootCts = null;
        }

        private void ClearTarget()
        {
            if (_currentTarget == null) return;
            
            _currentTarget.Health.OnDied -= OnCurrentTargetDiedHandler;
            _currentTarget = null;
        }
    }
}