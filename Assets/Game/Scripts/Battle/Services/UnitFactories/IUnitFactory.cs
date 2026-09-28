using Assets.Game.Scripts.Battle.Configs;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.UnitFactories
{
    public interface IUnitFactory
    {
        GameObject CreateUnit(UnitConfig config, Vector3 position, Entity entity, World world);
    }
}