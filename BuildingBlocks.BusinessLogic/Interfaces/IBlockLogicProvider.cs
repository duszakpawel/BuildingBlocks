using System;
using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IBlockLogicProvider
    {
        /// <summary>
        ///     Block logic class provider
        /// </summary>
        /// <param name="block">block object</param>
        /// <returns>collection of rotated block</returns>
        IList<Block> RotateBlock(Block block);

        /// <summary>
        ///     Returns best place for block
        /// </summary>
        /// <param name="board">board content</param>
        /// <param name="block">block content</param>
        /// <returns></returns>
        Tuple<int, int> FindBestPlaceForBlock(bool[,] board, bool[,] block);
    }
}