using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class Projectile : MonoBehaviour
    {
        private Enemy _target;
        private int _damage;
        private float _speed;
        private ParticleSystem _hitVFXPrefab;

        private Vector3 _targetLastPosition;
        private Vector3 _startPosition;

        private float _time;
        private float _flightTime;
        private float _arcHeight;
        
        private IVFXFactory _vfxFactory;

        [Inject]
        public void Construct(IVFXFactory vfxFactory) => _vfxFactory = vfxFactory;

        public void Init(Enemy target, int damage, float speed, float arcHeight, ParticleSystem hitVFXPrefab)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            _target = target;
            _damage = damage;
            _speed = speed;
            _hitVFXPrefab = hitVFXPrefab;

            _targetLastPosition = target.transform.position;
            _startPosition = transform.position;

            var distance = Vector3.Distance(_startPosition, _targetLastPosition);
            _flightTime = distance / _speed;
            _arcHeight = arcHeight;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_target != null)
            {
                _targetLastPosition = _target.transform.position;
            }

            var t = _time / _flightTime;

            t = Mathf.Clamp01(t);

            var horizontalPos = Vector3.Lerp(_startPosition, _targetLastPosition, t);

            var height = _arcHeight * 4 * (t - t * t);

            horizontalPos.y += height;

            transform.position = horizontalPos;

            if (!(t >= 1f))
                return;
            
            if (_target != null)
                _target.ApplyDamage(_damage);

            if (_hitVFXPrefab != null)
                _vfxFactory.Create(_hitVFXPrefab, transform.position);

            Destroy(gameObject);
        }
    }
}