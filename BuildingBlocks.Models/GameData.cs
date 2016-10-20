using System.Collections.Generic;

namespace BuildingBlocks.Models
{
    public class GameData
    {
        public int WellWidth { get; set; }

        public int? BlocksCount { get; set; }

        public List<Block> Blocks { get; set; }
    }
}
