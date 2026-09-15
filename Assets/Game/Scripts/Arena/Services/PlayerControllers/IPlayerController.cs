using Assets.Game.Scripts.Arena.Player;

namespace Assets.Game.Scripts.Arena.Services.PlayerControllers
{
    public interface IPlayerController
    {
        void Init(Joystick joystick, ArenaPlayer arenaPlayer);
        void IncreaseMovementSpeed(float upgradeEachLevelIncreaseCoefficient);
    }
}