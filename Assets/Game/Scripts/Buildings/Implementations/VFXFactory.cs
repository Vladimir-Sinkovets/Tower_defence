using Assets.Game.Scripts.Buildings.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class VFXFactory : IVFXFactory
    {
        private readonly IInstantiator _instantiator;

        public VFXFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public ParticleSystem Create(ParticleSystem vfxPrefab, Vector3 position)
        {
            var vfx = _instantiator.InstantiatePrefab(vfxPrefab).GetComponent<ParticleSystem>();
            vfx.transform.position = position;
                    
            vfx.Play();

            Object.Destroy(vfx.gameObject, vfx.main.duration);

            return vfx;
        }
    }
}