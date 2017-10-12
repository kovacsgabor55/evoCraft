﻿using System;

namespace EvoCraft.Common
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
