using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Movement
{
    [Serializable]
    public struct Position : IComponent { public Vector3 Value; }
    [Serializable]
    public struct MoveDirection : IComponent { public Vector3 Value; }
}