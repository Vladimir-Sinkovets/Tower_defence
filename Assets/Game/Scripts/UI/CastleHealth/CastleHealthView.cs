using UnityEngine;

namespace Assets.Game.Scripts.UI.CastleHealth
{
    public class CastleHealthView : MonoBehaviour, ICastleHealthView
    {
        [SerializeField] private Bar _bar;

        public void UpdateHealthBar(float value) => _bar.UpdateBar(value);
    }
}