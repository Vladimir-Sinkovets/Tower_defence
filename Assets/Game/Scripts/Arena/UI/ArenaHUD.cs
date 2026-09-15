using Assets.Game.Scripts.Arena.UI.Experience;
using Assets.Game.Scripts.Arena.UI.GameInfo;
using Assets.Game.Scripts.UI.HealthBar;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI
{
    public class ArenaHUD : MonoBehaviour
    {
        [field: SerializeField] public HealthBarView HealthBarView { get; private set; }
        [field: SerializeField] public ExperienceView ExperienceView { get; private set; }
        [field: SerializeField] public GameInfoView GameInfoView { get; private set; }
    }
}