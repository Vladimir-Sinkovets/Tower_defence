using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class MetaCurrencyView : MonoBehaviour, IMetaCurrencyView
    {
        [SerializeField] private TMP_Text _textField;

        public void SetText(string text) => _textField.text = text;
    }
}