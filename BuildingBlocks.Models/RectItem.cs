using System.Windows.Media;

namespace BuildingBlocks.Models
{
    public class RectItem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Brush FillColor { get; set; }
        public Brush StrokeColor { get; set; }

        public RectItem()
        {
        }

        public RectItem(int x, int y)
        {
            X = x;
            Y = y;
            Height = Constants.SingleTileWidth;
            Width = Constants.SingleTileWidth;
            FillColor = Constants.BlockFillColor;
            StrokeColor = Constants.BlockEdgeColor;
        }
    }
}