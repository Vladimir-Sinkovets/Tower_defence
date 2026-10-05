using System;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public event Action OnHit;
        
        public void OnHitAnimationEventHandler() => OnHit?.Invoke();
    }
}