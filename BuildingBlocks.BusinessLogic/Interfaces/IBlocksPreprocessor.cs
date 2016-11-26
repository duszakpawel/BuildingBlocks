using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    /// <summary>
    /// Block preprocessor interface
    /// </summary>
    public interface IBlocksPreprocessor
    {
        /// <summary>
        /// preprocess method (before displaying it in browser)
        /// </summary>
        /// <param name="blocks">blocks to preprocess</param>
        /// <param name="canvasWidth">canvas width</param>
        List<Block> Preprocess(List<Block> blocks, int canvasWidth);
    }
}