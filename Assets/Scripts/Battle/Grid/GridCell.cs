namespace Grid
{
    public class GridCell<T>
    {
        private T _CellData;
        private int _X;
        private int _Y;

        public GridCell(int x, int y)
        {
            _X = x;
            _Y = y;
            _CellData = default;
        }

        public void Place(T data)
        {
            _CellData = data;
        }
    }
}
