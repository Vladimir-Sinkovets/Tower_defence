using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public abstract class WeaponAnimation : MonoBehaviour
    {
        public abstract UniTask PlayBeforeAttackAnimation(CancellationToken ct);
    }
}