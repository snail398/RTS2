using System;
using Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BuildButtonView : View, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _BuildingImage;
        [SerializeField]
        private Text _MineralCostText;
        [SerializeField]
        private Text _GasCostText;
        [SerializeField]
        private GameObject _MineralCost;
        [SerializeField]
        private GameObject _GasCost;
        [SerializeField]
        private Text _Name;
        
        public event Action<PointerEventData, BuildingConfig> OnClick;
        public event Action<BuildingConfig> OnRelease;
        private BuildingConfig _BuildingConfig;

        public void Init(BuildingConfig building) {
            _BuildingConfig = building;
            _BuildingImage.sprite = _BuildingConfig.BuildingSprite;
            _Name.text = _BuildingConfig.Name;
            _MineralCost.SetActive(_BuildingConfig.MineralCost > 0);
            _GasCost.SetActive(_BuildingConfig.GasCost > 0);
            _MineralCostText.text = _BuildingConfig.MineralCost.ToString();
            _GasCostText.text = _BuildingConfig.GasCost.ToString();
        }

        public void OnPointerDown(PointerEventData eventData) {
            OnClick?.Invoke(eventData, _BuildingConfig);
        }

        public void OnPointerUp(PointerEventData eventData) {
            OnRelease?.Invoke(_BuildingConfig);
        }
    }
}