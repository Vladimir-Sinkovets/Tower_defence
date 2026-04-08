using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void UpdateBar(float value)
        {
            value = Mathf.Clamp01(value);

            _bar.fillAmount = value;
        }
    }
}