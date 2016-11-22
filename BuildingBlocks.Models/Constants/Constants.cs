using System.Collections.Generic;
using System.Windows.Media;

namespace BuildingBlocks.Models.Constants
{
    /// <summary>
    ///     Contains common const values to share
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     Width of single tile
        /// </summary>
        public const int SingleTileWidth = 25;

        /// <summary>
        ///     Canvas start height
        /// </summary>
        public const int SimulationStartHeight = 400;

        /// <summary>
        ///     Compulsory free space in well height
        /// </summary>
        public const int CompulsoryFreeSpaceInWellHeight = 8;

        /// <summary>
        ///     Height for counting density 
        /// </summary>
        public const int HeightForCountingDensity = 10;

        /// <summary>
        ///     Column Height Difference Multiplier 
        /// </summary>
        public const double ColumnHeightDifferenceMultiplier = 1.0;
        
        /// <summary>
        ///     Block positions counts
        /// </summary>
        public const int BlockPositionsCount = 1;
        
        /// <summary>
        ///     Recently inserted block color
        /// </summary>
        public static readonly Brush BlockFillColor = Brushes.DeepSkyBlue;

        /// <summary>
        ///     Block edge color
        /// </summary>
        public static readonly Brush BlockEdgeColor = Brushes.Black;

        /// <summary>
        ///     Inserted block color
        /// </summary>
        public static readonly Brush OldBlockFillColor = Brushes.Gray;

        /// <summary>
        ///     New block stroke color
        /// </summary>
        public static readonly Brush NewBlockStrokeColor = Brushes.Red;

        /// <summary>
        ///     New block stroke thickness
        /// </summary>
        public static readonly int NewBlockStrokeThickness = 5;

        /// <summary>
        ///     Sample Brushes
        /// </summary>
        public static List<Brush> FillBrushes { get; } = new List<Brush>
        {
            Brushes.Blue,
            Brushes.Green,
            Brushes.Red,
            Brushes.Orange,
            Brushes.Purple,
            Brushes.DeepPink,
            Brushes.Gold,
            Brushes.YellowGreen,
            Brushes.DeepSkyBlue,
            Brushes.Fuchsia,
            Brushes.Indigo,
            Brushes.Magenta,
            Brushes.Maroon,
            Brushes.Cyan,
            Brushes.Tomato,
            Brushes.Peru,
            Brushes.Azure,
            Brushes.Aqua
        };
    }
}