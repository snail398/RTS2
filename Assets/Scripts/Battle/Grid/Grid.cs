using UnityEngine;

namespace GridNamesapace
{
    public class Grid<T> where T : class
    {
        private readonly int _Width;
        private readonly int _Height;
        private readonly Vector2Int _Origin;
        private readonly GridCell<T>[,] _Cells;

        public int Width => _Width;
        public int Height => _Height;

        public Vector2Int Origin => _Origin;

        public Grid(int width, int height, Vector2Int origin)
        {
            _Width = width;
            _Height = height;
            _Origin = origin;
            
            _Cells = new GridCell<T>[_Width, _Height];
            for (var i = 0; i < _Width; i++)
            {
                for (var j = 0; j < _Height; j++)
                {
                    _Cells[i, j] = new GridCell<T>(i, j);
                }
            }
        }

        public void Place(int x, int y, T data)
        {
            _Cells[x, y].Place(data);
        }
        
        public bool IsUsed(int x, int y)
        {
            return _Cells[x, y].IsUsed;
        }
    }
}