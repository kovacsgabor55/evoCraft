using EvoCraft.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvoCraft.Core
{
    public class Barracks : TrainerBuilding
    {
        public static int FoodCost = 0;
        public static int GoldCost = 200;
        public static int WoodCost = 400;

        public Barracks(int PlayerId) : this(PlayerId, true) { }
        public Barracks(int PlayerId, bool UnderConstruction)
            : base("Barracks", 500, 20, PlayerId, null, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, FoodCost))
        { }
        

        internal override void FinishBuilding()
        {
            ActualHealthPoints = MaximalHealthPoints;
            PossibleActions = new List<Actions> { Actions.TrainSoldier, Actions.TrainPozsiHero, Actions.TrainGunMan, Actions.Cancel };
        }
    }
}