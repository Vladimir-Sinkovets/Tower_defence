using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.HudFactories
{
    public interface IHudFactory
    {
        UniTask CreateHUD(Health castleHealth);
    }
}