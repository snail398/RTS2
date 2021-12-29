using System;
using Battle.Buildings;
using GameObjectView;
using GridNamesapace;
using Zenject;

namespace Battle.Minerals
{
    public class MineralsService : IInitializable, IDisposable
    {
        private readonly MapView _MapView;
        private readonly IDataProvider<Grid<IBuildable>> _GridProvider;
        private readonly MineralFactory _MineralFactory;

        public MineralsService(MapView mapView, IDataProvider<Grid<IBuildable>> gridProvider, MineralFactory mineralFactory)
        {
            _MapView = mapView;
            _GridProvider = gridProvider;
            _MineralFactory = mineralFactory;
        }

        void IInitializable.Initialize()
        {
            foreach (var mineralZone in _MapView.MineralZones)
            {
                foreach (var mineralView in mineralZone.MineralsViews)
                {
                    var mineral = _MineralFactory.Create();
                    mineral.Initialize(mineralView.StartMineralAmount, mineralView);
                    _GridProvider.Data.Place(mineralView.GridPosition.x, mineralView.GridPosition.y, mineral);
                }
            }
        }

        void IDisposable.Dispose()
        {
        }
    }
}