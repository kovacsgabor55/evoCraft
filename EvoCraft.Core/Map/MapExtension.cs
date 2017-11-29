using EvoCraft.Common;

namespace EvoCraft.Core
{
    public static class MapExtension
    {
        /// <summary>
        /// Gets the position of the cell in the 2d space
        /// </summary>
        /// <param name="cell">The cell to test</param>
        /// <returns>The point at which the cell is</returns>
        public static Point getPosition(this Map map, Cell cell)
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    if (map.Cells[j + i * map.Height] == cell)
                    {
                        Point pt = new Point(i, j);
                        return pt;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a cell on the given position.
        /// </summary>
        public static Cell GetCellAt(this Map map, int x, int y)
        {
            return map.Cells[x + y * map.Height];
        }

        /// <summary>
        /// Gets a cell on the given position.
        /// </summary>
        public static Cell GetCellAt(this Map map, Point place)
        {
            return map.Cells[place.x + place.y * map.Height];
        }
    }
}
