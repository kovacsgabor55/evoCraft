using EvoCraft.Common.Map;

namespace EvoCraft.Common.MapObjects
{
    /// <summary>
    /// Base class for all objects that will be displayed on the map.
    /// </summary>
    public abstract class MapObject
    {
        
        /// <summary>
        /// Identification for every MapObject. It is unique, implementation is in MapObject class.
        /// </summary>
        public int Id
        {
            get;
        }

        /// <summary>
        /// A blocking object blocks any other blocking object. Only one block object can be on a map's cell at a time.
        /// The number of non-blocking objects can be placed on a cell is unlimited.
        /// The right implementation is required to be made in the Cell's definition.
        /// </summary>
        public BlockType BlockType
        {
            get; set;
        }

        /// <summary>
        /// The Z index determines in what order the Map objects will be drawn on the screen.
        /// The highest number should be drawn the last.
        /// </summary>
        public int ZIndex
        {
            get;
        }

        /// <summary>
        /// Will be displayed in the panel, indicating what is the name of the kind of map object.
        /// Should be set in the constructor.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Helps the id setting process.
        /// </summary>
        private static int idCoutner = 0;

        public MapObject(BlockType BlockType, int ZIndex, string Label)
        {
            this.ZIndex = ZIndex;
            this.BlockType = BlockType;
            this.Id = idCoutner;
            idCoutner++;
            this.Label = Label;
        }

        public virtual void Update() { }

        public virtual void Update(Point pos) { }
    }
}
