using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    public interface IUnitFactory
    {
        GameObject CreateUnit(Vector3 position, Entity entity, World world);
    }
}