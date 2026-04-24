using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class HealthBarMediator : MonoBehaviour
    {
        [SerializeField] private Bar _bar;
        [SerializeField] private Health _health;

        // private void Awake() => _health.OnHpChanged += OnHpChangedHandler;
        //
        // private void OnHpChangedHandler(int currentHp, int maxHp) => _bar.UpdateBar(currentHp / (float)maxHp);
        //
        // private void OnDestroy() => _health.OnHpChanged -= OnHpChangedHandler;
    }
}