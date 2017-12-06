namespace EvoCraft.Common.MapObjects.PlayerControlled.Buildings
{
    public class MedicalTent : TrainerBuilding
    {
        public static int FoodCost = 200;
        public static int GoldCost = 25;
        public static int WoodCost = 50;

        public MedicalTent(int PlayerId) : this(PlayerId, true) { }
        public MedicalTent(int PlayerId, bool UnderConstruction)
            : base("Medical tent", 100, 10, PlayerId, null, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, FoodCost))
        { }
    }
}
