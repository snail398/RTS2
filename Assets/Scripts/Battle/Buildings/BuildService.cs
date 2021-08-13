using System;
using Battle.Actions;
using Common;
using Grid;
using UnityEngine;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Battle.Buildings
{
    public class BuildService : IInitializable, IDisposable
    {
        private readonly UnityEventProvider _UnityEventProvider;
        private readonly IDataProvider<Grid<IBuildable>> _GridProvider;
        private readonly ResourceLoaderService _ResourceLoaderService;

        public BuildService(UnityEventProvider unityEventProvider, IDataProvider<Grid<IBuildable>> gridProvider, ResourceLoaderService resourceLoaderService)
        {
            _UnityEventProvider = unityEventProvider;
            _GridProvider = gridProvider;
            _ResourceLoaderService = resourceLoaderService;
        }

        public void Initialize()
        {
            _UnityEventProvider.OnUpdate += Update;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                //fire build action
                var clickPos = MouseUtils.GetMouseWorldPosititon(LayerMask.GetMask("Ground"));
                var gridPos = GridUtils.GetXY(_GridProvider.Data, clickPos);
                var action = new BuildAction("CommandCenter", gridPos.x, gridPos.y);
                Build(action);
            }
        }

        private void Build(BuildAction buildAction)
        {
            var b = _ResourceLoaderService.LoadResource<GameObject>($"Prefabs/Buildings/{buildAction.BuildingName}");
            Object.Instantiate(b, new Vector3(buildAction.X, 0.12f, buildAction.Y), Quaternion.identity);
        }
        
        public void Dispose()
        {
            _UnityEventProvider.OnUpdate -= Update;
        }
    }
}