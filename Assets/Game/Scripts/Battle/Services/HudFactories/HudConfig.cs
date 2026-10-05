using Assets.Game.Scripts.Battle.UI;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.HudFactories
{
    [CreateAssetMenu(fileName = "Hud_config", menuName = "Battle/Hud config")]
    public class HudConfig : ScriptableObject
    {
        public BattleHud HUD;
    }
}