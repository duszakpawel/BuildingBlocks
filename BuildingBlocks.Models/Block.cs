using System.Collections.Generic;
using System.Windows.Shapes;

namespace BuildingBlocks.Models
{
    public class Block
    {
        public int Width { get; set; }

        public int Heigth { get; set; }

        public bool[,] Content { get; set; }

        public List<Rectangle> CanvasChildren { get; set; }
    }
}
