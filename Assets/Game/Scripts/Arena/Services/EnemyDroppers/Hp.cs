using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Hp : Drop
    {
        public void Init() => PlayAppearanceAnimation();
        
        protected override void ApplyBonus()
        {
            Debug.Log("Applying hp bonus");
        }
    }
}