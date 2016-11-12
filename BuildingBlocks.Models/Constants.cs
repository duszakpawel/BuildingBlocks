using System.Windows.Media;

namespace BuildingBlocks.Models
{
    public class Constants
    {
        public static readonly Brush BlockFillColor = Brushes.DeepSkyBlue;
        public static readonly Brush BlockEdgeColor = Brushes.Black;
        public static readonly Brush OldBlockEdgeColor = Brushes.Gray;

        public const int SingleTileWidth = 25;
        public const int SimulationStartHeight = 400;
        public const int CompulsoryFreeSpaceInWellHeight = 8; // in bool[,] size 



    }
}
