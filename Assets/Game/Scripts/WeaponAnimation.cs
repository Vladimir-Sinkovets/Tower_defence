using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public abstract class WeaponAnimation : MonoBehaviour
    {
        public abstract IEnumerator PlayBeforeAttackAnimation();
    }
}