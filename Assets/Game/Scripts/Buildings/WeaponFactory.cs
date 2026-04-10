using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class WeaponFactory : ScriptableObject
    {
        public abstract Weapon Create(DiContainer container);
    }
}