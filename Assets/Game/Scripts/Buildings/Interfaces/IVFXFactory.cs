using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IVFXFactory
    {
        ParticleSystem Create(ParticleSystem vfxPrefab, Vector3 position);
    }
}