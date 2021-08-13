using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Buildings;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridService : IInitializable, IDisposable, IDataProvider<Grid<IBuildable>>
    {
        private Grid<IBuildable> _BuildingsGrid;
        public Grid<IBuildable> Data => _BuildingsGrid;

        public void Initialize()
        {
            _BuildingsGrid = new Grid<IBuildable>(10, 10, Vector2Int.zero);
        }

        public void Dispose()
        {

        }

    }
}
