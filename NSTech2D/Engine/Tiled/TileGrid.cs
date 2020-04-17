
namespace NSTech2D.Engine.Tiled
{
    class TileGrid
    {
        private int rows;
        private int cols;
        private TileInstance[] tiles;

        public TileGrid(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            tiles = new TileInstance[rows * cols];
        }

        public TileInstance At(int index)
        {
            return tiles[index];
        }

        public void Set(int row, int col, TileInstance inst)
        {
            tiles[row * cols + col] = inst;
        }

        public int Size()
        {
            return tiles.Length;
        }
    }
}
