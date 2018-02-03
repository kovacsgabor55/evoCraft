using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using System.Linq;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class TrainerBuildingExtension
    {
        public static void Update(this TrainerBuilding trainerBuilding, Point pos)
        {
            //MapObjectExtension.Update(trainerBuilding, pos);
            if (trainerBuilding.TrainingQueue.Count > 0)
            {
                if (trainerBuilding.TrainingQueue.ElementAt(0).TrainingTime > 0)
                {
                    trainerBuilding.TrainingQueue.ElementAt(0).TrainingTime--;
                }
                else
                {
                    Unit newUnit = trainerBuilding.TrainingQueue.ElementAt(0);
                    newUnit.MoveTarget = trainerBuilding.SpawnTarget;
                    trainerBuilding.TrainingQueue.RemoveAt(0);
                    Engine.SpawnUnitFromBuilding(trainerBuilding, newUnit);
                }
            }
            if (trainerBuilding.IsUnderConstruction && trainerBuilding.BuildTime == 0)
            {
                //trainerBuilding.IsUnderConstruction = false;
                //trainerBuilding.FinishBuilding();
            }
        }

        public static void Update(this TrainerBuilding trainerBuilding)
        {
            Update(trainerBuilding, new Point(0, 0));
        }

        /// <summary>
        /// Starts making the unit if the player has enough resources.
        /// </summary>
        public static void StartMakingUnit(this TrainerBuilding trainerBuilding, Unit unit)
        {
            if (Engine.ThePlayer.Resources.HasEnoughToReduceBy(unit.Costs))
            {
                Engine.ThePlayer.Resources.ReduceBy(unit.Costs);
                trainerBuilding.TrainingQueue.Add(unit);
            }
        }

        public static int GetNumberOfUnitsInTheQueue(this TrainerBuilding trainerBuilding)
        {
            return trainerBuilding.TrainingQueue.Count;
        }

        /// <summary>
        /// Cancels all training.
        /// </summary>
        public static void CancelTraining(this TrainerBuilding trainerBuilding)
        {
            foreach (Unit u in trainerBuilding.TrainingQueue)
            {
                Engine.ThePlayer.Resources.Add(u.Costs);
            }
            trainerBuilding.TrainingQueue.Clear();
        }
    }
}
