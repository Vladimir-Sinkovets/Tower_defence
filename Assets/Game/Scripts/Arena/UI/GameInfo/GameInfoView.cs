using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.UI.GameInfo
{
    public class GameInfoView : MonoBehaviour, IGameInfoView
    {
        [SerializeField] private TMP_Text _id;
        
        public void SetId(string id) => _id.text = $"Room id: {id}";
    }
}