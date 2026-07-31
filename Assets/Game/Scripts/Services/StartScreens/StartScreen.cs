using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Services.StartScreens
{
    public class StartScreen : MonoBehaviour, IStartScreen
    {
        [SerializeField] private TMP_Text _text;
        
        public void ShowMessage(string message) => _text.text = message;
    }
}