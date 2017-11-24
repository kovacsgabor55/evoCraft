using System;
using EvoCraft.Common;

namespace EvoCraft.Core
{
    /// <summary>
    /// Meant to represent trees  
    /// </summary>
    public class Tree : Resource
    {
        public Tree():base("Tree", 105, BlockType.BlockOtherBlock) { Type = ResourceType.Wood; }

        public bool HasFullCapacity()
        {
            return MaxCapacity == Capacity;
        }
    }
}
