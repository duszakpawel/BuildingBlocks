using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Models
{
    public class Block
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool[,] Content { get; set; }
    }
}
