namespace EvoCraft.Common.MapObjects.PlayerControlled.Buildings
{
    public class FarmBuilding : Building
    {
        public static int FoodCost = 0;
        public static int GoldCost = 0;
        public static int WoodCost = 150;

        public FarmBuilding(int PlayerId) : this(PlayerId, true) { }
        public FarmBuilding(int PlayerId, bool UnderConstruction)
            : base("Farm", 100, 10, PlayerId, null, UnderConstruction, 3, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {
            if (!UnderConstruction)
            {
                FinishBuilding();
            }
        }

        // it changes into a farm resource when it is finished.
        public override void FinishBuilding()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);

            if (found)
            {
                Engine.Map.GetCellAt(pos).MapObjects.Add(new Farm());
                Engine.DestroyMapObject(this);
            }
        
        }
    }
}