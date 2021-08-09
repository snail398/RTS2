using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _Width;
    private int _Height;
    private GridCell[,] _Cells;

    public Grid(int width, int height)
    {
        _Width = width;
        _Height = height;

        _Cells = new GridCell[_Width, _Height];
        for (var i = 0; i < _Width; i++)
        {
            for (var j = 0; j < _Height; j++)
            {
                _Cells[i, j] = new GridCell(i, j);
            }
        }
    }
}
