using EvoCraft.Common;

namespace EvoCraft.Core
{
    /// <summary>
    /// Basic building that blocks people.
    /// </summary>
    public class Wall : Building
    {
        public static int FoodCost = 0;
        public static int GoldCost = 0;
        public static int WoodCost = 100;

        /// <summary>
        /// Make new wall
        /// </summary>
        public Wall(int PlayerId) : this(PlayerId, true) { }
        public Wall(int PlayerId, bool UnderConstruction) 
            : base("Wall", 300, 10, PlayerId, null, UnderConstruction, 3, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {
            if (!UnderConstruction)
            {
                FinishBuilding();
            }
        }
    }
}
