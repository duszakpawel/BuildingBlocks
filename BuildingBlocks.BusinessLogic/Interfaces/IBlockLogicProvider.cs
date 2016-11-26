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
        ///     Returns best places for block (X, Y) coordinates for left top corner of block
        /// </summary>
        /// <param name="board">board content</param>
        /// <param name="block">block content</param>
        /// <returns></returns>
        List<Tuple<int, int>> FindBestPlacesForBlock(int[,] board, bool[,] block);
    }
}