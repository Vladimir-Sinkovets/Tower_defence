using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.MainMenuStatistics
{
    public class MainMenuStatisticsView : MonoBehaviour, IMainMenuStatisticsView
    {
        [SerializeField] private TMP_Text _metaCurrencyTextField;
        [SerializeField] private TMP_Text _recordTextField;

        public void SetMetaCurrency(string text) => _metaCurrencyTextField.text = text;
        public void SetWavesRecord(string text) => _recordTextField.text = text;
    }
}