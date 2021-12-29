using UnityEngine;
using GridNamesapace;

namespace Utils
{
    public static class GridUtils
    {
        public static Vector2Int GetXY<T>(this Grid<T> grid, Vector3 position) where T : class
        {
            return new Vector2Int((int)(position.x - grid.Origin.x), (int)(position.z - grid.Origin.y));
        }
    }
}