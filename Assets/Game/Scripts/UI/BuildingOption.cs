using Assets.Game.Scripts.Buildings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI
{
    public class BuildingOption : MonoBehaviour
    {
        public event Action<BuildingOption> OnClick;

        [SerializeField] private Image _icon;

        private BuildingConfig _config;

        public BuildingConfig Config { get => _config; }

        public void Init(BuildingConfig config)
        {
            _config = config;
            _icon.sprite = config.Icon;
        }

        public void OnClickHandler() => OnClick?.Invoke(this);

        private void OnDestroy()
        {
            OnClick = null;
        }
    }
}