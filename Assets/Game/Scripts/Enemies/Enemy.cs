using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected Health health;

        public Health Health => health;

        public abstract void Activate(Health target);

        public abstract void Deactivate();
    }
}