using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Input
{
    public struct ClickEvent : IComponent { public Vector2 ScreenPosition; }
    public struct ClickOnFieldEvent : IComponent { public Vector3 Position; }
}