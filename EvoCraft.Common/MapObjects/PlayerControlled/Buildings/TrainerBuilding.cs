using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using System.Collections.Generic;
using System.Linq;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Buildings
{
    public abstract class TrainerBuilding : Building
    {
        public List<Unit> TrainingQueue;
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
    }
}
