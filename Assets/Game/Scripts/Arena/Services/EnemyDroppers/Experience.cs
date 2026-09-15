using Assets.Game.Scripts.Arena.Services.Experiences;
using Photon.Pun;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Experience : Drop
    {
        private IExperienceService _experienceService;

        private int _experience;

        [Inject]
        public void Construct(IExperienceService experienceService) => _experienceService = experienceService;

        public void Init(int experience)
        {
            PlayAppearanceAnimation();

            PhotonView.RPC(nameof(InitRpc), RpcTarget.All, experience);
        }

        [PunRPC]
        public void InitRpc(int experience) => _experience = experience;

        protected override void ApplyBonus() => _experienceService.Increase(_experience);
    }
}