using System.Collections.Generic;
using System.Windows.Shapes;

namespace BuildingBlocks.Models
{
    public class Block
    {
        public int Width { get; set; }

        public int Heigth { get; set; }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= 0) _quantity = value;
            }
        }

        public bool[,] Content { get; set; }

        public List<Rectangle> CanvasChildren { get; set; }

        private int _quantity;
    }
}
