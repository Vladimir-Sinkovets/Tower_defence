using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
using Photon.Pun;
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

            PhotonView.RPC(nameof(InitRpc), RpcTarget.All, heal);
        }

        [PunRPC]
        public void InitRpc(int heal) => _heal = heal;

        protected override void ApplyBonus() =>
            _playerAccessor.CurrentPlayer.Health.ApplyHeal(_heal);
    }
}