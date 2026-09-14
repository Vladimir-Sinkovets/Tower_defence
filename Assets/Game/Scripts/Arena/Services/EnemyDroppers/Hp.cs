using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Hp : Drop
    {
        private IPlayerAccessor _playerAccessor;
        
        private int _heal;

        [Inject]
        public void Construct(IPlayerAccessor playerAccessor) => _playerAccessor = playerAccessor;

        public void Init(int heal)
        {
            PlayAppearanceAnimation();
            _heal = heal;
        }

        protected override void ApplyBonus() =>
            _playerAccessor.CurrentPlayer.Health.ApplyHeal(_heal);
    }
}