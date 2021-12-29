using System;
using Common;
using Settings;
using UnityEngine;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Battle.Buildings
{
    public class DragService : IInitializable, IDisposable
    {
        private readonly SignalBus _SignalBus;
        private readonly UnityEventProvider _UnityEventProvider;
        private readonly ResourceLoaderService _ResourceLoaderService;
        private readonly BuildService _BuildService;

        private GameObject _DraggedObject; // ToDO: make IDraggable
        private BuildingConfig _DraggedConfig;

        public DragService(SignalBus signalBus, UnityEventProvider unityEventProvider, ResourceLoaderService resourceLoaderService, BuildService buildService)
        {
            _SignalBus = signalBus;
            _UnityEventProvider = unityEventProvider;
            _ResourceLoaderService = resourceLoaderService;
            _BuildService = buildService;
        }

        void IInitializable.Initialize()
        {
            _SignalBus.Subscribe<TryStartBuildingSignal>(OnTryStartBuilding, this);
            _SignalBus.Subscribe<ReleaseCurrentBuildingSignal>(OnReleaseCurrentBuilding, this);
        }

        private void OnTryStartBuilding(TryStartBuildingSignal obj)
        {
            _DraggedConfig = obj.BuildingConfig;
            var mousePosition = MouseUtils.GetMouseWorldPosititon(LayerMask.GetMask("Ground")) - new Vector3((float)_DraggedConfig.Width / 2, 0 , (float)_DraggedConfig.Height / 2);
            _DraggedObject = Object.Instantiate(obj.BuildingConfig.Prefab, mousePosition, Quaternion.identity).gameObject;
            _UnityEventProvider.OnUpdate += DragProcess;
        }

        private void DragProcess()
        {
            var mousePosition = MouseUtils.GetMouseWorldPosititon(LayerMask.GetMask("Ground")) - new Vector3((float)_DraggedConfig.Width / 2, 0 , (float)_DraggedConfig.Height / 2);
            _DraggedObject.transform.position = mousePosition;
            _SignalBus.FireSignal(new DragProcessSignal(_DraggedConfig, mousePosition));
        }

        private void OnReleaseCurrentBuilding(ReleaseCurrentBuildingSignal obj)
        {
            _UnityEventProvider.OnUpdate -= DragProcess;       
            var mousePosition = MouseUtils.GetMouseWorldPosititon(LayerMask.GetMask("Ground")) - new Vector3((float)_DraggedConfig.Width / 2, 0 , (float)_DraggedConfig.Height / 2);
            _BuildService.TryMouseBuild(_DraggedConfig, mousePosition);
            Object.Destroy(_DraggedObject);
        }

        void IDisposable.Dispose()
        {
            _SignalBus.UnSubscribeFromAll(this);
        }
    }
}