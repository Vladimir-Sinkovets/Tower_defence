using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    public class ArenaPlayer : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public PhotonView PhotonView { get; private set; }
        
        public Vector3 Position => transform.position;

        public void TakeDamage(float damage)
        {
            Debug.Log($"{gameObject.name} get {damage} damage");
        }
    }
}