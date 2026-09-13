using Assets.Game.Scripts.Arena.Buildings.ShootingBuildings;
using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.HealthBar;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Player
{
    public class ArenaPlayer : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBarView;
        
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public PhotonView PhotonView { get; private set; }
        [field: SerializeField] public ShootingBuilding ShootingBuilding { get; private set; }
        public Vector3 Position => transform.position;
        public Health Health { get; private set; }
        
        private HealthBarPresenter _presenter;
        private Registry<ArenaPlayer> _playerRegistry;

        [Inject]
        public void Construct(Registry<ArenaPlayer> playerRegistry)
        {
            _playerRegistry = playerRegistry;
            playerRegistry.Register(this);
        }
        
        public void Init(int hp)
        {
            Health = new Health(hp);
            
            _presenter = new HealthBarPresenter(Health, _healthBarView);
            
            _presenter.Init();
        }
        
        public void IncreaseHp(int hp) => Health.AddHp(hp);

        private void OnDestroy()
        {
            _playerRegistry.Unregister(this);
            _presenter?.Dispose();
        }

        public void ApplyDamage(int configDamage) => 
            PhotonView.RPC(nameof(TakeDamage), PhotonView.Controller, configDamage);

        [PunRPC]
        private void TakeDamage(int configDamage) => Health.ApplyDamage(configDamage);
    }
}