using System;
using EvoCraft.Common;

namespace EvoCraft.Core
{
    /// <summary>
    /// Meant to represent farms. 
    /// </summary>
    public class Farm: Resource
    {
        public Farm():base("Farm", 300, BlockType.BlockOtherBlock)
        {
            Type = ResourceType.Food;
        }
    }
}
