using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.HealthFeature
{
    public struct Health : IComponent
    {
        public int Hp;
    }
    
    public struct Dead : IComponent { }
}