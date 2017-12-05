using EvoCraft.Common.MapObjects;
using System.Collections.Generic;

namespace EvoCraft.Common.Map
{
    /// <summary>
    /// A cell is a cell of the map's 2d array. It can contain many Mapobjects.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The list of the MapObjects. Can be any kind of unit.
        /// </summary>
        public List<MapObject> MapObjects;

        /// <summary>
        /// The ground's texture.
        /// </summary>
        public GroundTexture Ground
        {
            get; set;
        }

        /// <summary>
        /// A láthatóságot tároló rész. Egyelőre csak az egyetlen játékosnak tárolja.
        /// </summary>
        public VisibilityType Visibility
        {
            get { return myVisiblitly; }
            set { myVisiblitly = value; }
        }

        /// <summary>
        /// Egy cella létrehozása véletlenszerű földdel.
        /// </summary>
        public Cell(int variantRandomNum)
        {
            MapObjects = new List<MapObject>();
            switch (variantRandomNum)
            {
                case 0: Ground = GroundTexture.Grass1; break;
                case 1: Ground = GroundTexture.Grass2; break;
                case 2: Ground = GroundTexture.Grass3; break;
                case 3: Ground = GroundTexture.Grass4; break;
                case 4: Ground = GroundTexture.Grass5; break;
                default: Ground = GroundTexture.Grass1; break;
            }
            Visibility = VisibilityType.Unexplored;
        }

        private VisibilityType myVisiblitly;
    }
}
