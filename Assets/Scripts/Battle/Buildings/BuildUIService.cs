using System;
using System.Collections.Generic;
using Settings;
using UI;
using UI.Panels;
using UnityEngine.EventSystems;
using Zenject;
using Object = UnityEngine.Object;

namespace Battle.Buildings
{
    public class BuildUIService : IInitializable, IDisposable
    {
        private readonly UIService _UiService;
        private readonly SignalBus _SignalBus;
        private readonly ResourceLoaderService _ResourceLoaderService;

        private BuildingsConfig _BuildingConfig;
        private readonly List<BuildButtonView> _ActiveBuildButton = new List<BuildButtonView>();
        private BuildButtonView _BuildButtonPrefab;

        public BuildUIService(UIService uiService, SignalBus signalBus, ResourceLoaderService resourceLoaderService)
        {
            _UiService = uiService;
            _SignalBus = signalBus;
            _ResourceLoaderService = resourceLoaderService;
        }

        void IInitializable.Initialize()
        {
            _BuildingConfig = _ResourceLoaderService.LoadResource<BuildingsConfig>($"Settings/BuildingsConfig");
            _BuildButtonPrefab = _ResourceLoaderService.LoadResource<BuildButtonView>($"Prefabs/UI/BuildButton");

            DrawBuildingButtons();
        }

        private void DrawBuildingButtons()
        {
            ReleaseCurrentButton();
            var buildButton = Object.Instantiate(_BuildButtonPrefab, _UiService.GetPanel<BuildingsPanel>().transform);
            buildButton.Init(_BuildingConfig.Buildings[0]);
            buildButton.OnClick += TryStartBuilding;
            buildButton.OnRelease += ReleaseCurrentBuilding;
            _ActiveBuildButton.Add(buildButton);
        }

        private void TryStartBuilding(PointerEventData clickData, BuildingConfig buildingConfig)
        {
            _SignalBus.FireSignal(new TryStartBuildingSignal(buildingConfig));
        }

        private void ReleaseCurrentBuilding(BuildingConfig buildingConfig)
        {
            _SignalBus.FireSignal(new ReleaseCurrentBuildingSignal());
        }

        private void ReleaseCurrentButton(bool delete = true)
        {
            foreach (var buttonView in _ActiveBuildButton)
            {
                buttonView.OnClick -= TryStartBuilding;
                buttonView.OnRelease -= ReleaseCurrentBuilding;
                if (delete)
                    Object.Destroy(buttonView.gameObject);
            }
            _ActiveBuildButton.Clear();
        }
        
        void IDisposable.Dispose()
        {
            ReleaseCurrentButton(false);
        }
    }

    public class TryStartBuildingSignal : ISignal
    {
        public readonly BuildingConfig BuildingConfig;

        public TryStartBuildingSignal(BuildingConfig buildingConfig)
        {
            BuildingConfig = buildingConfig;
        }
    }
    
    public class ReleaseCurrentBuildingSignal : ISignal {}
}