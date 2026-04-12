using Assets.Game.Scripts.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.UI
{
    public class MetaCurrencyCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;

        private MetaCurrencyService _metaCurrencyService;

        [Inject]
        private void Construct(MetaCurrencyService metaCurrencyService) => _metaCurrencyService = metaCurrencyService;

        private void Start() => _textField.text = _metaCurrencyService.Total.ToString();
    }
}