using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using System;
using System.Collections;
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
        [SerializeField] private float _rotationSpeed = 360.0f;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _beforeShootAnimation;


        private GameContext _context;
        private Damageable _currentTarget;

        private float _nextShootTime = 0;

        [Inject]
        private void Construct(GameContext context)
        {
            _context = context;
        }

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
            _nextShootTime = Time.time + _attackInterval;

            if (_beforeShootAnimation != null)
                yield return _beforeShootAnimation.PlayBeforeAttackAnimation();

            var projectile = Instantiate(_projectilePrefab);

            projectile.transform.position = _projectileStartPosition.transform.position;

            projectile.Init(_currentTarget, _damage, _projectileSpeed);

            yield break;
        }

        private void FindTarget()
        {
            foreach (var enemy in _context.All)
            {
                if (enemy.IsDied)
                    continue;

                if (Vector3.Distance(enemy.transform.position, transform.position) <= _attackRadius)
                {
                    _currentTarget = enemy;

                    _currentTarget.OnDied += OnCurrentTargetDiedHandler;

                    _nextShootTime = Time.time + _attackInterval;

                    break;
                }
            }
        }

        private void RotateWeapon()
        {
            if (_currentTarget == null) return;

            Vector3 direction = _currentTarget.transform.position - _weaponRoot.position;
            direction.y = 0f; // Игнорируем наклон по вертикали

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // Плавный поворот с максимальной скоростью _rotationSpeed
                _weaponRoot.rotation = Quaternion.RotateTowards(
                    _weaponRoot.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
            }
        }


        private void OnCurrentTargetDiedHandler()
        {
            _currentTarget.OnDied -= OnCurrentTargetDiedHandler;

            _currentTarget = null;
        }
    }
}