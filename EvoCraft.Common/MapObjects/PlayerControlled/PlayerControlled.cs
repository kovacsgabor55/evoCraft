using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled
{
    /// <summary>
    /// Common abstraction for all player controlled units.
    /// </summary>
    public abstract class PlayerControlledClass : MapObject
    {
        /// <summary>
        /// The maximal Health Points of the building
        /// </summary>
        public int MaximalHealthPoints { get; set; }

        /// <summary>
        /// The actual Health Points of the building
        /// </summary>
        public int ActualHealthPoints { get; set; }

        /// <summary>
        /// The id of the player that owns the building.
        /// </summary>
        public int PlayerId { get;  set; }

        /// <summary>
        /// The range of the sight of the given object.
        /// </summary>
        public int SightRange { get;  set; }

        /// <summary>
        /// The cost of the thing
        /// </summary>
        public ResourceSet Costs { get; set; }

        public List<Actions> PossibleActions
        {
            get; set;
        }

        public Actions NextAxtion
        {
            get; set;
        }

        public PlayerControlledClass(
            int PlayerId, 
            int MaximalHealthPoints, 
            int ActualHealthPoints,
            int SightRange, 
            BlockType BlockT, 
            int Zindex, 
            string Label,
            ResourceSet Costs,
            List<Actions> PossibleActions,
            Actions NextAxtion) 
            : base(BlockT, Zindex, Label)
        {
            this.PlayerId = PlayerId;
            this.MaximalHealthPoints = MaximalHealthPoints;
            this.ActualHealthPoints = ActualHealthPoints;
            this.SightRange = SightRange;
            this.Costs = Costs;
            this.PossibleActions = PossibleActions;
            this.NextAxtion = NextAxtion;
        }
    }
}
