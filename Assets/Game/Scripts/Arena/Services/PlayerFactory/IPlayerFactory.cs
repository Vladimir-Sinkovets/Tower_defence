using Assets.Game.Scripts.Arena.Player;

namespace Assets.Game.Scripts.Arena.Services.PlayerFactory
{
    public interface IPlayerFactory
    {
        ArenaPlayer CreatePlayer();
    }
}