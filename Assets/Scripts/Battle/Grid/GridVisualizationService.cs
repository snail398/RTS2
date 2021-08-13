using UnityEngine;
using Zenject;
using System;
using Battle.Buildings;
using GameObjectView;
using Object = UnityEngine.Object;

namespace Grid
{
    public class GridVisualizationService : IInitializable, IDisposable
    {
        private readonly IDataProvider<Grid<IBuildable>> _GridProvider;
        private readonly ResourceLoaderService _ResourceLoaderService;

        private TileView _TilePrefab;
        private GameObject _TileRoot;

        public GridVisualizationService(IDataProvider<Grid<IBuildable>> gridProvider, ResourceLoaderService resourceLoaderService)
        {
            _GridProvider = gridProvider;
            _ResourceLoaderService = resourceLoaderService;
        }

        public void Initialize()
        {
            _TileRoot = new GameObject("TilePrefab");
            _TilePrefab = _ResourceLoaderService.LoadResource<TileView>("Prefabs/Tile");
            var buildingGrid = _GridProvider.Data;
            DrawGrid(buildingGrid);
        }

        private void DrawGrid(Grid<IBuildable> buildingGrid)
        {
            for (int i = 0; i < buildingGrid.Width; i++)
            {
                for (int j = 0; j < buildingGrid.Height; j++)
                {
                    var tilePosition = new Vector3(buildingGrid.Origin.x + i, 0.11f, buildingGrid.Origin.y + j);
                    var tile = Object.Instantiate(_TilePrefab, _TileRoot.transform);
                    tile.transform.position = tilePosition;
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
