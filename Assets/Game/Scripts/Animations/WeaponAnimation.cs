using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public abstract class WeaponAnimation : MonoBehaviour
    {
        public abstract IEnumerator PlayBeforeAttackAnimation();
    }
}