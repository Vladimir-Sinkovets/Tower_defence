using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public interface IDropper
    {
        void Drop(Vector3 position, DropConfig dropConfig);
    }
}