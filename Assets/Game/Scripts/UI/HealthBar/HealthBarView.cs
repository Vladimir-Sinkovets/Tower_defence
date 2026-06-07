using UnityEngine;

namespace Assets.Game.Scripts.UI.HealthBar
{
    public class HealthBarView : MonoBehaviour, IHealthBarView
    {
        [SerializeField] private Bar _bar;
        
        public void UpdateBar(float currentHp) => _bar.UpdateBar(currentHp);
    }
}