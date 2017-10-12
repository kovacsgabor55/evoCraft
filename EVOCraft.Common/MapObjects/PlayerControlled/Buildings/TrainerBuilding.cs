using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Common
{
    public abstract class TrainerBuilding : Building
    {
        private List<Unit> TrainingQueue;
        public Point SpawnTarget;

        public TrainerBuilding(
            string Label,
            int MaximalHealthPoints,
            int BuildTime,
            int PlayerId,
            List<Actions> possibleActions,
            bool IsUnderConstruction,
            int SightRange,
            ResourceSet Costs)
            : base(Label, MaximalHealthPoints, BuildTime, PlayerId, possibleActions, IsUnderConstruction, SightRange, Costs)
        {
            TrainingQueue = new List<Unit>();
            SpawnTarget = null;
            if (!IsUnderConstruction)
            {
                FinishBuilding();
            }
        }

        public int GetNumberOfUnitsInTheQueue()
        {
            return TrainingQueue.Count;
        }

        /// <summary>
        /// Starts making the unit if the player has enough resources.
        /// </summary>
        public void StartMakingUnit(Unit unit)
        {
            if (Engine.ThePlayer.Resources.HasEnoughToReduceBy(unit.Costs))
            {
                Engine.ThePlayer.Resources.ReduceBy(unit.Costs);
                TrainingQueue.Add(unit);
            }
        }

        public override void Update()
        {
            if (TrainingQueue.Count > 0)
            {
                if (TrainingQueue.ElementAt(0).TrainingTime > 0)
                {
                    TrainingQueue.ElementAt(0).TrainingTime--;
                }
                else
                {
                    Unit newUnit = TrainingQueue.ElementAt(0);
                    newUnit.MoveTarget = SpawnTarget;
                    TrainingQueue.RemoveAt(0);
                    Engine.SpawnUnitFromBuilding(this, newUnit);
                }
            }
            if (IsUnderConstruction && BuildTime == 0)
            {
                IsUnderConstruction = false;
                FinishBuilding();
            }
        }

        /// <summary>
        /// Cancels all training.
        /// </summary>
        public void CancelTraining()
        {
            foreach (Unit u in TrainingQueue)
            {
                Engine.ThePlayer.Resources.Add(u.Costs);
            }
            TrainingQueue.Clear();
        }

    }
}
