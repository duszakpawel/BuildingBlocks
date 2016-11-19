using System.Windows.Media;

namespace BuildingBlocks.Models.Constants
{
    /// <summary>
    /// Contains common const values to share
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Recently inserted block color
        /// </summary>
        public static readonly Brush BlockFillColor = Brushes.DeepSkyBlue;

        /// <summary>
        /// Block edge color
        /// </summary>
        public static readonly Brush BlockEdgeColor = Brushes.Black;

        /// <summary>
        /// Inserted block color
        /// </summary>
        public static readonly Brush OldBlockEdgeColor = Brushes.Gray;

        /// <summary>
        /// Width of single tile
        /// </summary>
        public const int SingleTileWidth = 25;

        /// <summary>
        /// Canvas start height
        /// </summary>
        public const int SimulationStartHeight = 400;

        /// <summary>
        /// Compulsory free space in well height
        /// </summary>
        public const int CompulsoryFreeSpaceInWellHeight = 8; 
    }
}
