using EvoCraft.Common;
using System.Collections.Generic;

namespace EvoCraft.Core
{
    /// <summary>
    /// Holds all the data in the map in a 2d array of cells and the basic properties of the map.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The List for the 2d array which holds all the data on the map.
        /// </summary>
        public List<Cell> Cells
        {
            get
            {
                return cells;
            }
        }
        /// <summary>
        /// Horizontal size of the map
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
        }
        /// <summary>
        /// Vertical size of the map
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
        }
        /// <summary>
        /// The initial gold of the player.
        /// </summary>
        public int InitialGold
        {
            get
            {
                return myInitialGold;
            }
        }
        /// <summary>
        /// The initial wood of the player.
        /// </summary>
        public int InitialWood
        {
            get
            {
                return myInitialWood;
            }
        }
        /// <summary>
        /// The initial food of the player.
        /// </summary>
        public int InitialFood
        {
            get
            {
                return myInitialFood;
            }
        }
        /// <summary>
        /// The title of the map
        /// </summary>
        public string Title
        {
            get
            {
                return myTitle;
            }
        }
        /// <summary>
        /// The author of the map
        /// </summary>
        public string Author
        {
            get
            {
                return myAuthor;
            }
        }

        /// <summary>
        /// Create a map with the sizes.
        /// </summary>
        /// <param name="height">Horizontal size</param>
        /// <param name="width">Vertical size</param>
        public Map(int width, int height)
            : this("","",width,height,100,100,100)
        {
        }

        /// <summary>
        /// Create a map with the sizes, and other properties.
        /// </summary>
        /// <param name="height">Horizontal size</param>
        /// <param name="width">Vertical size</param>
        public Map(string title, string author, int width, int height, int initialGold, int initialFood, int initialWood)
        {
            this.height = height;
            this.width = width;
            myTitle = title;
            myAuthor = author;
            myInitialFood = initialFood;
            myInitialGold = initialGold;
            myInitialWood = initialWood;
            cells = new List<Cell>(height * width);

            System.Random rand = new System.Random();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    cells.Add(new Cell(rand.Next(19)));
                }
            }
        }

        /// <summary>
        /// Gets the position of the cell in the 2d space
        /// </summary>
        /// <param name="cell">The cell to test</param>
        /// <returns>The point at which the cell is</returns>
        public Point getPosition(Cell cell)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Cells[j + i * Height] == cell)
                    {
                        Point pt = new Point(i,j);
                        return pt;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a cell on the given position.
        /// </summary>
        public Cell GetCellAt(int x, int y)
        {
            return Cells[x + y * Height];
        }

        /// <summary>
        /// Gets a cell on the given position.
        /// </summary>
        public Cell GetCellAt(Point place)
        {
            return Cells[place.x + place.y * Height];
        }

        private List<Cell> cells;
        private int height;
        private int width;
        private int myInitialGold;
        private int myInitialWood;
        private int myInitialFood;
        private string myTitle;
        private string myAuthor;
    }
}
