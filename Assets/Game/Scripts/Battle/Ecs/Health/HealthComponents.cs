using System;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature
{
    [Serializable]
    public struct Health : IComponent
    {
        public int Hp;
    }
}