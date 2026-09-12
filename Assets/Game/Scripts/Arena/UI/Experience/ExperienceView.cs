using Assets.Game.Scripts.UI;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI.Experience
{
    public class ExperienceView : MonoBehaviour, IExperienceView
    {
        [SerializeField] private Bar _bar;
        
        public void SetExperienceBar(float value) => _bar.UpdateBar(value);
    }
}