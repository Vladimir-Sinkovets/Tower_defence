using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class BuildingFactory : ScriptableObject
    {
        public float RadiusOfOccupiedSpace = 1.0f;
        public abstract Building Create(IInstantiator instantiator);
    }
}