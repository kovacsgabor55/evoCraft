using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Buildings
{
    public class Barracks : TrainerBuilding
    {
        public static int FoodCost = 0;
        public static int GoldCost = 200;
        public static int WoodCost = 400;
        private List<Actions> list;
        private int v1;
        private bool v2;

        public Barracks(int PlayerId) : this(PlayerId, true) { }
        public Barracks(int PlayerId, bool UnderConstruction)
            : base("Barracks", 500, 20, PlayerId, null, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, FoodCost))
        {
            PossibleActions = new List<Actions> { Actions.TrainSoldier, Actions.TrainPozsiHero, Actions.TrainGunMan, Actions.TrainDoctor, Actions.Cancel };
        }

        public Barracks(int PlayerId, bool UnderConstruction, List<Actions> list)
            : base("Barracks", 500, 20, PlayerId, list, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, FoodCost))
        {
        }
    }
}