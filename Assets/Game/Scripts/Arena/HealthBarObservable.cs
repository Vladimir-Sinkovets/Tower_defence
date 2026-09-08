using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Arena
{
    public class HealthBarObservable : MonoBehaviour, IPunObservable
    {
        [SerializeField] private Image _healthBar;
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_healthBar.fillAmount);
            }
            else
            {
                _healthBar.fillAmount = (float)stream.ReceiveNext();
            }
        }
    }
}