using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class Weapon : MonoBehaviour
    {
        private Registry<Weapon> _weaponRegistry;

        [Inject]
        protected virtual void Construct(Registry<Weapon> weaponRegistry) => _weaponRegistry = weaponRegistry;

        public virtual void Init() => _weaponRegistry.Register(this);

        public abstract void Stop();

        protected virtual void OnDestroy() => _weaponRegistry.Unregister(this);
    }
}