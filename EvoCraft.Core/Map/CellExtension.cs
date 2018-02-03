using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;

namespace EvoCraft.Core
{
    public static class CellExtension
    {
        /// <summary>
        /// Based on the object to be moved it tells if you can move the object there.
        /// </summary>
        /// <returns>Wether a block object can move to the cell.</returns>
        public static bool canMapObjectBePlaced(this Cell cell, MapObject givenMapObj)
        {
            bool cantPlace = false;
            foreach (MapObject mo in cell.MapObjects)
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
        public static bool canBlockTypeBePlaced(this Cell cell, BlockType blockType)
        {
            bool cantPlace = false;
            foreach (MapObject mo in cell.MapObjects)
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
    }
}
