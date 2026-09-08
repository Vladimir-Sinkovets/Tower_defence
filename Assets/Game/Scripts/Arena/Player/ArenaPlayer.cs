using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.HealthBar;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Player
{
    public class ArenaPlayer : MonoBehaviour
    {
        [SerializeField] public HealthBarView _healthBarView;
        
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public PhotonView PhotonView { get; private set; }
        public Vector3 Position => transform.position;
        public Health Health { get; private set; }
        
        private HealthBarPresenter _presenter;
        
        public void Init(int hp)
        {
            Health = new Health(hp);
            
            _presenter = new HealthBarPresenter(Health, _healthBarView);
            
            _presenter.Init();
        }

        private void OnDestroy() => _presenter.Dispose();
    }
}