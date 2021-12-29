using System;
using Battle.Buildings;
using GameObjectView;
using UnityEngine;
using Zenject;

namespace GridNamesapace
{
    public class GridService : IInitializable, IDisposable, IDataProvider<Grid<IBuildable>>
    {
        private readonly MapView _MapView;
        private Grid<IBuildable> _BuildingsGrid;
        Grid<IBuildable> IDataProvider<Grid<IBuildable>>.Data => _BuildingsGrid;

        public GridService(MapView mapView)
        {
            _MapView = mapView;
        }

        void IInitializable.Initialize()
        {
            _BuildingsGrid = new Grid<IBuildable>(60, 20, Vector2Int.zero);
            var impassableCellBuilding = new ImpassableCellBuilding();
            foreach (var cell in _MapView.ImpassibleCells.Cells)
            {
                _BuildingsGrid.Place(cell.x, cell.y, impassableCellBuilding);
            }
        }

        void IDisposable.Dispose()
        {

        }

    }
}
