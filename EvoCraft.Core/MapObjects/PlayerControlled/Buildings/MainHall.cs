using System.Collections.Generic;

namespace EvoCraft.Core
{
    public class MainHall : TrainerBuilding
    {
        public static int FoodCost = 0;
        public static int GoldCost = 300;
        public static int WoodCost = 300;

        public MainHall(int PlayerId) : this(PlayerId, true) { }
        public MainHall(int PlayerId, bool UnderConstruction)
            : base("Main Hall", 700, 30, PlayerId, null, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, FoodCost))
        { }
        

        internal override void FinishBuilding()
        {
            ActualHealthPoints = MaximalHealthPoints;
            PossibleActions = new List<Actions> { Actions.TrainWorker, Actions.Cancel };
        }
    }
}
