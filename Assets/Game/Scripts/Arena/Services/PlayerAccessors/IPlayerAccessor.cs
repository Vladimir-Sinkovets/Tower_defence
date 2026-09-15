using Assets.Game.Scripts.Arena.Player;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.PlayerAccessors
{
    public interface IPlayerAccessor
    {
        ArenaPlayer GetNearestTarget(Vector3 point);
        ArenaPlayer CurrentPlayer { get; }
        void SetCurrentPlayer(ArenaPlayer player);
    }
}