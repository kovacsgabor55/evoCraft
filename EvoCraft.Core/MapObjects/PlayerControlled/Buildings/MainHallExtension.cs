using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using System.Collections.Generic;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class MainHallExtension
    {
        public static void FinishBuilding(this MainHall mainHall)
        {
            mainHall.ActualHealthPoints = mainHall.MaximalHealthPoints;
            mainHall.PossibleActions = new List<Actions> { Actions.TrainWorker, Actions.Cancel };
        }
    }
}
