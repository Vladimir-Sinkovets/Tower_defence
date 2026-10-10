using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Shared;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    public class HealthObservable : MonoBehaviour, IPunObservable
    {
        [SerializeField] private ArenaPlayer _player;
        
        private Health _health;

        private void Awake() => _health = _player.Health;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (_health == null)
                return;
            
            if (stream.IsWriting)
            {
                stream.SendNext((_health.CurrentHp, _health.StartHp));
            }
            else
            {
                var value = ((int CurrentHp, int StartHp))stream.ReceiveNext();
                
                _health.Set(value.CurrentHp,  value.StartHp);
            }
        }
    }
}