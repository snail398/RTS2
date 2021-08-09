using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    private IBuildable _Building;
    private int _X;
    private int _Y;

    public GridCell(int x, int y)
    {
        _X = x;
        _Y = y;
        _Building = null;
    }

    public void PlaceBuilding(IBuildable building)
    {
        _Building = building;
    }
}
