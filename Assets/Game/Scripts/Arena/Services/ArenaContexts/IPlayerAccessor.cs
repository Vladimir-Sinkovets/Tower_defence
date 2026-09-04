using System.Collections.Generic;

namespace Assets.Game.Scripts.Arena.Services.ArenaContexts
{
    public interface IPlayerAccessor
    {
        IEnumerable<ArenaPlayer> Players { get; }
        void UpdatePlayers();
    }
}