using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Input
{
    [Serializable]
    public struct ClickEvent : IComponent { public Vector2 ScreenPosition; }
    [Serializable]
    public struct ClickOnFieldEvent : IComponent { public Vector3 Position; }
}