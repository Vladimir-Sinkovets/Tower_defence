using System.Collections.Generic;
using Assets.Game.Scripts.Arena.Player;

namespace Assets.Game.Scripts.Arena.Services.ArenaContexts
{
    public interface IPlayerAccessor
    {
        IEnumerable<ArenaPlayer> Players { get; }
        void UpdatePlayers();
    }
}