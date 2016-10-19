using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Models
{
    public class GameData
    {
        public int WellWidth { get; set; }
        public int? BlocksCount { get; set; }
        public List<Block> Blocks { get; set; }
    }
}
