using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using System.Collections.Generic;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class MedicalTentExtension
    {
        public static void FinishBuilding(this MedicalTent medicalTent)
        {
            medicalTent.ActualHealthPoints = medicalTent.MaximalHealthPoints;
            medicalTent.PossibleActions = new List<Actions> { Actions.TrainDoctor, Actions.Cancel };
        }
    }
}
