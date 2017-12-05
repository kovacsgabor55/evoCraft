namespace EvoCraft.Common.MapObjects.Resources
{
    /// <summary>
    /// Base class of all resources, abstract class.
    /// </summary>
    public abstract class Resource : MapObject
    {
        /// <summary>
        /// This method returns with the value given in "amount" parameter, but it can be less if the given value isn't available.
        /// </summary>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public int gatherAmount(int amount)
        {
            int returnAmount = 0;
            if (Capacity > amount)
            {
                returnAmount = amount;
            }
            else
            {
                returnAmount = Capacity;
            }
            Capacity -= returnAmount;
            return returnAmount;
        }

        public ResourceType Type;

        public override void Update()
        {
            if (Capacity <= 0)
            {
                Engine.DestroyMapObject(this);
            }
        }
        /// <summary>
        /// The amount that stores this resource
        /// </summary>
        public int Capacity { get; protected public set; }
        /// <summary>
        /// The maximum storage capacity for this resource
        /// </summary>
        protected public int MaxCapacity { get; set; }

        /// <summary>
        /// The Z index of all resources. Helps the drawing engine determine which what order to draw the objects.
        /// </summary>
        private static readonly int ResourceZIndex = 1;

        /// <summary>
        /// 
        /// </summaryemert>
        /// <param name="maxCapacity"></param>
        /// <param name="capacity"></param>
        public Resource(string Label, int maxCapacity, BlockType BlockType) : base(BlockType, ResourceZIndex, Label)
        {
            MaxCapacity = maxCapacity;
            Capacity = maxCapacity;
        }
    }
}
