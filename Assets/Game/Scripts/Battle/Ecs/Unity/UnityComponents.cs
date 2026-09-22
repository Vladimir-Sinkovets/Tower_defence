using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity
{
    public struct TransformComponent : IComponent { public Transform Reference; }
    public struct AnimatorComponent : IComponent { public Animator Reference; }
}