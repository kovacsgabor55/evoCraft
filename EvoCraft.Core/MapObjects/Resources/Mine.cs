using System;
using EvoCraft.Common;

namespace EvoCraft.Core
{
    /// <summary>
    /// Meant to represent mines
    /// </summary>
    public class Mine:Resource
    {
        //       _
        //   ___(o)>
        //  \ <_. )
        //   `---'  

        public Mine(int capacity) : base("Mine", capacity, BlockType.BlockOtherBlock) {
            Type = ResourceType.Gold;
        }
    }
}
