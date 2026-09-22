using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Movement
{
    public struct Position : IComponent { public Vector3 Value; }
    public struct Rotation : IComponent { public Quaternion Value; }
    public struct MoveDirection : IComponent { public Vector3 Value; }
}