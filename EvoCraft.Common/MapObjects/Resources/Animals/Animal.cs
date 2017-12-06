using System;
using EvoCraft.Common.Map;

namespace EvoCraft.Common.MapObjects.Resources.Animals
{
    /// <summary>
    /// Meant to represent animals
    /// </summary>
    abstract public class Animal: Resource
    {
        /// <summary>
        /// The maximal Health Points
        /// </summary>
        public int MaximalHealthPoints { get; set; }
        /// <summary>
        /// The actual Health Points
        /// </summary>
        public int ActualHealthPoints { get; set; }
        public bool Dead;

        public Animal(string Label, int maxCapacity, int maximalHealthPoints) : base(Label, maxCapacity, BlockType.BlockOtherBlock)
        {
            Dead = false;
            Type = ResourceType.Food;
            MaximalHealthPoints = maximalHealthPoints;
            ActualHealthPoints = maximalHealthPoints;
        }

        public int deccayDelay = 0;        
    }
}
