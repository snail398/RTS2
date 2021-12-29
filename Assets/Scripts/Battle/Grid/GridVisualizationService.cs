using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using Battle.Buildings;
using GameObjectView;
using Utils;
using Object = UnityEngine.Object;

namespace GridNamesapace
{
    public class GridVisualizationService : IInitializable, IDisposable
    {
        private readonly IDataProvider<Grid<IBuildable>> _GridProvider;
        private readonly ResourceLoaderService _ResourceLoaderService;
        private readonly SignalBus _SignalBus;
        
        private TileView[,] _CellViews;
        private TileView _TilePrefab;
        private GameObject _TileRoot;
        private Material _NormalTile;
        private Material _EmptyTile;
        private Material _UsedTile;
        private bool IsVisible;

        private readonly List<TileView> _HighlightedTiles = new List<TileView>();

        public GridVisualizationService(IDataProvider<Grid<IBuildable>> gridProvider, ResourceLoaderService resourceLoaderService, SignalBus signalBus)
        {
            _GridProvider = gridProvider;
            _ResourceLoaderService = resourceLoaderService;
            _SignalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _TileRoot = new GameObject("TilePrefab");
            _TilePrefab = _ResourceLoaderService.LoadResource<TileView>("Prefabs/Tile");
            _NormalTile = _ResourceLoaderService.LoadResource<Material>("Materials/NormalTile");
            _EmptyTile = _ResourceLoaderService.LoadResource<Material>("Materials/EmptyTile");
            _UsedTile = _ResourceLoaderService.LoadResource<Material>("Materials/UsedTile");
            var buildingGrid = _GridProvider.Data;
            CreateGrid(buildingGrid);
            SetGridVisibility(false);
            _SignalBus.Subscribe<TryStartBuildingSignal>(OnTryStartBuilding, this);
            _SignalBus.Subscribe<DragProcessSignal>(OnDragProcess, this);
            _SignalBus.Subscribe<ReleaseCurrentBuildingSignal>(OnReleaseCurrentBuilding, this);
        }

        private void OnTryStartBuilding(TryStartBuildingSignal obj)
        {
            SetGridVisibility(true);
        }

        private void OnReleaseCurrentBuilding(ReleaseCurrentBuildingSignal obj)
        {
            foreach (var highlightedTile in _HighlightedTiles)
            {
                highlightedTile.MeshRenderer.sharedMaterial = _NormalTile;
            }
            _HighlightedTiles.Clear();
            SetGridVisibility(false);
        }

        private void OnDragProcess(DragProcessSignal obj)
        {
            foreach (var highlightedTile in _HighlightedTiles)
            {
                highlightedTile.MeshRenderer.sharedMaterial = _NormalTile;
            }
            _HighlightedTiles.Clear();
            if (obj.MousePosition.x < 0 || obj.MousePosition.z < 0 || obj.MousePosition.x > _GridProvider.Data.Width - 1 || obj.MousePosition.z > _GridProvider.Data.Height)
                return;
            var baseTilePos = GridUtils.GetXY(_GridProvider.Data, obj.MousePosition);
            for (int i = baseTilePos.x; i < baseTilePos.x + obj.BuildingConfig.Width; i++)
            {
                for (int j = baseTilePos.y; j < baseTilePos.y + obj.BuildingConfig.Height; j++)
                {
                    var tile = _CellViews[i, j];
                    var material = _GridProvider.Data.IsUsed(i, j) ? _UsedTile : _EmptyTile;
                    tile.MeshRenderer.sharedMaterial = material;
                    _HighlightedTiles.Add(tile);
                }
            }
        }

        private void CreateGrid(Grid<IBuildable> buildingGrid)
        {
            _CellViews  = new TileView[buildingGrid.Width, buildingGrid.Height];
            for (int i = 0; i < buildingGrid.Width; i++)
            {
                for (int j = 0; j < buildingGrid.Height; j++)
                {
                    var tilePosition = new Vector3(buildingGrid.Origin.x + i, 0.11f, buildingGrid.Origin.y + j);
                    var tile = Object.Instantiate(_TilePrefab, _TileRoot.transform);
                    tile.MeshRenderer.sharedMaterial = _NormalTile;
                    tile.transform.position = tilePosition;
                    _CellViews[i, j] = tile;
                }
            }

            IsVisible = true;
        }

        private void SetGridVisibility(bool isVisible)
        {
            if (IsVisible == isVisible)
            {
                Debug.LogError($"Grid visibility already is {isVisible} ");
                return;
            }
            IsVisible = isVisible;
            for (int i = 0; i < _CellViews.GetLength(0); i++)
            {
                for (int j = 0; j < _CellViews.GetLength(1); j++)
                {
                    _CellViews[i, j].gameObject.SetActive(isVisible);
                }
            }
        }
        
        void IDisposable.Dispose()
        {
           _SignalBus.UnSubscribeFromAll(this); 
        }
    }
}
