using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Battle.UI.UnitsPanel
{
    public class UnitOptionView : MonoBehaviour
    {
        public event Action<UnitOptionView> OnClicked;
        
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _highlighter;

        public string Id { get; private set; }

        private void Awake() => _button.onClick.AddListener(OnClickedHandler);

        private void OnClickedHandler() => OnClicked?.Invoke(this);

        public void SetId(string id) => Id = id;
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        public void SetTitle(string title) => _title.text = title;
        public void Select() => _highlighter.SetActive(true);
        public void Deselect() => _highlighter.SetActive(false);
    }
}