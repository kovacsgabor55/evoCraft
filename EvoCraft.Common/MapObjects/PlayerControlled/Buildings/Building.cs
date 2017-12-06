using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Buildings
{
    /// <summary>
    /// Meant to represent buildings.
    /// </summary>
    public abstract class Building : PlayerControlledClass
    {
        public bool IsUnderConstruction { get; set; }
        
        /// <summary>
        /// Time required for building to finish. Can be decreased as it is built.
        /// </summary>
        public int BuildTime { get; set; }
        
        public int InitialBuildTime { get; set; }

        /// <summary>
        /// The Z index of all ground textures.
        /// </summary>
        public static readonly int BuildingZIndex = 3;

        public Building(
            string Label, 
            int MaximalHealthPoints, 
            int BuildTime, 
            int PlayerId, 
            List<Actions> possibleActions, 
            bool IsUnderConstruction, 
            int SightRange,
            ResourceSet Costs)
        : base(PlayerId, MaximalHealthPoints, 1, SightRange, BlockType.BlockOtherBlock, BuildingZIndex, Label, Costs, possibleActions, Actions.None)
        {
            if (IsUnderConstruction)
            {
                this.BuildTime = BuildTime;
            }
            else
            {
                this.BuildTime = 0;
            }
            this.IsUnderConstruction = IsUnderConstruction;
            this.BuildTime = BuildTime;
            this.InitialBuildTime = BuildTime;
        }

        public virtual void FinishBuilding()
        {
            ActualHealthPoints = MaximalHealthPoints;
        }

        public override void Update()
        {
            if (IsUnderConstruction && BuildTime == 0)
            {
                IsUnderConstruction = false;
                FinishBuilding();
            }
        }
    }
}
