using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class GridVisualizationService : IInitializable, IDisposable
{
    private readonly IDataProvider<Grid> _GridProvider;

    public GridVisualizationService(IDataProvider<Grid> gridProvider)
    {
        _GridProvider = gridProvider;
    }

    public void Initialize()
    {
        Debug.Log("loaded");
    }

    public void Dispose()
    {
    }
}
