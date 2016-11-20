using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.Models.Serializable
{
    /// <summary>
    ///     Game data
    /// </summary>
    public class GameData
    {
        /// <summary>
        ///     Well width
        /// </summary>
        public int WellWidth { get; set; }

        /// <summary>
        ///     Blocks count
        /// </summary>
        public int? BlocksCount { get; set; }

        /// <summary>
        ///     Collection of blocks
        /// </summary>
        public List<Block> Blocks { get; set; } = new List<Block>();
    }
}