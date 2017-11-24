using System.Collections.Generic;
using EvoCraft.Common;

namespace EvoCraft.Core
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

        /// <summary>
        /// Based on the object to be moved it tells if you can move the object there.
        /// </summary>
        /// <returns>Wether a block object can move to the cell.</returns>
        public bool canMapObjectBePlaced(MapObject givenMapObj)
        {
            bool cantPlace = false;
            foreach (MapObject mo in MapObjects)
            {
                switch (mo.BlockType)
                {
                    case BlockType.BlockAll:
                        cantPlace = true;
                        break;
                    case BlockType.BlockOtherBlock:
                        switch (givenMapObj.BlockType)
                        {
                            case BlockType.BlockAll:
                            case BlockType.BlockOtherBlock:
                                cantPlace = true;
                                break;
                        }
                        break;
                    case BlockType.NoBlock:
                        switch (givenMapObj.BlockType)
                        {
                            case BlockType.BlockAll:
                                cantPlace = true;
                                break;
                        }
                        break;

                }
            }
            return !cantPlace;
        }

        /// <summary>
        /// Based on the object to be moved it tells if you can move the object there bro.
        /// </summary>
        /// <returns>Wether a block object can move to the cell.</returns>
        public bool canBlockTypeBePlaced(BlockType blockType)
        {
            bool cantPlace = false;
            foreach (MapObject mo in MapObjects)
            {
                switch (mo.BlockType)
                {
                    case BlockType.BlockAll:
                        cantPlace = true;
                        break;
                    case BlockType.BlockOtherBlock:
                        switch (blockType)
                        {
                            case BlockType.BlockAll:
                            case BlockType.BlockOtherBlock:
                                cantPlace = true;
                                break;
                        }
                        break;
                    case BlockType.NoBlock:
                        switch (blockType)
                        {
                            case BlockType.BlockAll:
                                cantPlace = true;
                                break;
                        }
                        break;

                }
            }
            return !cantPlace;
        }


        private VisibilityType myVisiblitly;
    }
}
