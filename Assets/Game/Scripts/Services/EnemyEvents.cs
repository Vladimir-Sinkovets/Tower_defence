using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class EnemyEvents : ScriptableObject
    {
        public event Action OnEnemyDied;

        public void EnemyDie()
        {
            OnEnemyDied?.Invoke();
        }
    }
}