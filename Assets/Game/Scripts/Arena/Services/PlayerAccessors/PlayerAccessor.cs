using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Services.Registries;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.PlayerAccessors
{
    public class PlayerAccessor : IPlayerAccessor
    {
        private readonly Registry<ArenaPlayer> _playerRegistry;

        public PlayerAccessor(Registry<ArenaPlayer> playerRegistry) => _playerRegistry = playerRegistry;

        public ArenaPlayer GetNearestTarget(Vector3 point)
        {
            var distance = float.MaxValue;

            ArenaPlayer nearestTarget = null;
            
            foreach (var player in _playerRegistry.All)
            {
                if (Vector3.Distance(player.Position, point) <= distance)
                {
                    nearestTarget = player;
                    
                    distance = Vector3.Distance(player.Position, point);
                }
            }
            
            return nearestTarget;
        }
    }
}