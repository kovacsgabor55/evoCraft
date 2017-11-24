namespace EvoCraft.Common
{
    /// <summary>
    /// Tells wether an object can be on the same cell with other cells. <para/>
    /// CanEnterBuildings - Allows a unit (e.g. worker) to get into the MainHall or buildings in progress. <para/>
    /// BlockOtherBlock - Blocks other BlockOtherBlocks or BlockAlls, but allows for any NoBlock.<para/>
    /// BlockAll - Block every other block or no block or block all.<para/>
    /// Decoration - Is not affected by any of the others.<para/>
    /// </summary>
    public enum BlockType { NoBlock, BlockOtherBlock, BlockAll, Decoration };
}
