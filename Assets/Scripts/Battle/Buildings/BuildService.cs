using System;
using Battle.Actions;
using GameObjectView;
using GridNamesapace;
using Settings;
using UnityEngine;
using Utils;
using Zenject;
using Object = UnityEngine.Object;

namespace Battle.Buildings
{
    public class BuildService : IInitializable, IDisposable
    {
        private readonly IDataProvider<Grid<IBuildable>> _GridProvider;
        private readonly ResourceLoaderService _ResourceLoaderService;
        private readonly SignalBus _SignalBus;
        private readonly BuildingFactory _BuildingFactory;

        public BuildService(IDataProvider<Grid<IBuildable>> gridProvider, ResourceLoaderService resourceLoaderService, SignalBus signalBus, BuildingFactory buildingFactory)
        {
            _GridProvider = gridProvider;
            _ResourceLoaderService = resourceLoaderService;
            _SignalBus = signalBus;
            _BuildingFactory = buildingFactory;
        }

        void IInitializable.Initialize() { }

        public void TryMouseBuild(BuildingConfig buildingConfig, Vector3 mousePosition)
        {
            if (mousePosition.x < 0 || mousePosition.z < 0 || mousePosition.x > _GridProvider.Data.Width - 1 || mousePosition.z > _GridProvider.Data.Height)
                return;
            var baseTilePos = GridUtils.GetXY(_GridProvider.Data, mousePosition);
            if (!CanBuildHere(buildingConfig.Width, buildingConfig.Height, baseTilePos))
                return;
            var action = new BuildAction(buildingConfig.Name, baseTilePos.x, baseTilePos.y, buildingConfig.Width, buildingConfig.Height, 0);
            BuildProcess(action);
        }

        public void TryDirectBuild(BuildingConfig buildingConfig, Vector2Int gridPosition, byte playerId)
        {
            if (!CanBuildHere(buildingConfig.Width, buildingConfig.Height, gridPosition))
                return;
            var action = new BuildAction(buildingConfig.Name, gridPosition.x, gridPosition.y, buildingConfig.Width, buildingConfig.Height, playerId);
            BuildProcess(action);
        }

        private bool CanBuildHere(int width, int height, Vector2Int baseTilePos)
        {
            var emptyCellCount = width * height;
            for (int i = baseTilePos.x; i < baseTilePos.x + width; i++)
            {
                for (int j = baseTilePos.y; j < baseTilePos.y + height; j++)
                {
                    if (!_GridProvider.Data.IsUsed(i, j))
                        emptyCellCount--;
                }
            }

            if (emptyCellCount > 0)
                return false;
            return true;
        }


        private void BuildProcess(BuildAction buildAction)
        {
            var buildingViewPrefab = _ResourceLoaderService.LoadResource<BuildingView>($"Prefabs/Buildings/{buildAction.BuildingName}");
            var buildingView = Object.Instantiate(buildingViewPrefab, new Vector3(buildAction.X, 0.12f, buildAction.Y), Quaternion.identity);
            var building = _BuildingFactory.CreateBuilding(buildingView.BuildingType);
            if (building == null)
            {
                Debug.LogError($"error on creating building {buildingView.BuildingType}");
                return;
            }
            building.Initialize(buildingView, buildAction.PlayerId, buildAction.Width, buildAction.Height);
            
            for (int i = buildAction.X; i < buildAction.X + buildAction.Width; i++)
            {
                for (int j = buildAction.Y; j < buildAction.Y + buildAction.Height; j++)
                {
                    _GridProvider.Data.Place(i, j, building);
                }
            }
            _SignalBus.FireSignal(new BuildingCreatedSignal(building, buildAction.PlayerId));
        }
        
        void IDisposable.Dispose() { }
    }
}