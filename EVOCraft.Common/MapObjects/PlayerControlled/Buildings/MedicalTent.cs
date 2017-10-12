using System.Collections.Generic;

namespace EvoCraft.Common
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
        
        internal override void FinishBuilding()
        {
            ActualHealthPoints = MaximalHealthPoints;
            PossibleActions = new List<Actions> { Actions.TrainDoctor, Actions.Cancel };
        }

    }
}
