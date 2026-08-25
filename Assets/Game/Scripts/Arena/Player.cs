using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public PhotonView PhotonView { get; private set; }
    }
}