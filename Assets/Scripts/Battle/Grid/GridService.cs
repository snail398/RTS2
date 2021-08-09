using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridService : IInitializable, IDisposable, IDataProvider<Grid>
{
    private Grid _Grid;
    public Grid Data => _Grid;

    public void Initialize()
    {
        _Grid = new Grid(5, 5);
    }

    public void Dispose()
    {
    
    }

}
