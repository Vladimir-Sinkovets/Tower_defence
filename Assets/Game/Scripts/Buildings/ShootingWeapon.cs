using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingWeapon : Weapon
    {
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _beforeShootAnimation;


        private Registry<Enemy> _enemiesRegistry;
        private Enemy _currentTarget;

        private float _nextShootTime = 0;
        private ShootingWeaponFactory _config;

        [Inject]
        private void Construct(Registry<Enemy> enemyRegistry) => _enemiesRegistry = enemyRegistry;

        public void Init(ShootingWeaponFactory config) => _config = config;

        private void Update()
        {
            if (_currentTarget != null)
            {
                RotateWeapon();
                Attack();
            }
            else
            {
                FindTarget();
            }
        }

        private void Attack()
        {
            if (_nextShootTime > Time.time)
                return;

            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            _nextShootTime = Time.time + _config.AttackInterval;

            if (_beforeShootAnimation != null)
                yield return _beforeShootAnimation.PlayBeforeAttackAnimation();

            var projectile = Instantiate(_config.ProjectilePrefab);

            projectile.transform.position = _projectileStartPosition.transform.position;

            projectile.Init(_currentTarget, _config.Damage, _config.ProjectileSpeed, _config.ArcHeight);

            yield break;
        }

        private void FindTarget()
        {
            foreach (var enemy in _enemiesRegistry.All)
            {
                if (enemy.Health.IsDied)
                    continue;

                if (Vector3.Distance(enemy.transform.position, transform.position) <= _config.AttackRadius)
                {
                    _currentTarget = enemy;

                    _currentTarget.Health.OnDied += OnCurrentTargetDiedHandler;

                    _nextShootTime = Time.time + _config.AttackInterval;

                    break;
                }
            }
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


        private void OnCurrentTargetDiedHandler()
        {
            _currentTarget.Health.OnDied -= OnCurrentTargetDiedHandler;

            _currentTarget = null;
        }
    }
}