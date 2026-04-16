using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }

        public abstract void Activate(Health target);

        public abstract void Deactivate();
    }
}