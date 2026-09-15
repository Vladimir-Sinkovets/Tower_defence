using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI
{
    public class ArenaInput : MonoBehaviour
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
    }
}