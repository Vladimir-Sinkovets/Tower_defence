using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Experience : Drop
    {
        private int _experience;

        public void Init(int experience)
        {
            _experience = experience;

            PlayAppearanceAnimation();
        }

        protected override void ApplyBonus()
        {
            Debug.Log($"{nameof(Experience)}: {_experience}");
        }
    }
}