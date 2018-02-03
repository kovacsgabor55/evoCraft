using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using System.Collections.Generic;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class BarracksExtension
    {
        public static void FinishBuilding(this Barracks barracks)
        {
            barracks.ActualHealthPoints = barracks.MaximalHealthPoints;
            //barracks.PossibleActions = new List<Actions> { Actions.TrainSoldier, Actions.TrainPozsiHero, Actions.TrainGunMan, Actions.Cancel };
        }

        public static void Update(this Barracks trainerBuilding, Point pos)
        {
            TrainerBuildingExtension.Update(trainerBuilding, pos);
        }
    }
}
