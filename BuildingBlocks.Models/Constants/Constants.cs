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
        /// default step value
        /// </summary>
        public static int StepDefaultValue { get; } = 1;

        /// <summary>
        /// k default value
        /// </summary>
        public static int KDefaultValue { get; } = 1;

        /// <summary>
        ///     Width of single tile
        /// </summary>
        public static int SingleTileWidth { get; } = 25;

        /// <summary>
        ///     Canvas start height
        /// </summary>
        public static int SimulationStartHeight { get; } = 400;

        /// <summary>
        ///     Compulsory free space in well height
        /// </summary>
        public static int CompulsoryFreeSpaceInWellHeight { get; } = 15;

        /// <summary>
        ///     Height for counting density 
        /// </summary>
        public static int HeightForCountingDensity { get; } = 10;

        /// <summary>
        ///     Column Height Difference Multiplier 
        /// </summary>
        public static double ColumnHeightDifferenceMultiplier { get; } = 1.0;
        
        /// <summary>
        ///     Block positions counts
        /// </summary>
        public static int BlockPositionsCount { get; } = 1;
        
        /// <summary>
        ///     Recently inserted block color
        /// </summary>
        public static Brush BlockFillColor { get; } = Brushes.DeepSkyBlue;

        /// <summary>
        ///     Block edge color
        /// </summary>
        public static Brush BlockEdgeColor { get; } = Brushes.Black;

        /// <summary>
        ///     Inserted block color
        /// </summary>
        public static Brush OldBlockFillColor { get; } = Brushes.Gray;

        /// <summary>
        ///     New block stroke color
        /// </summary>
        public static Brush NewBlockStrokeColor { get; } = Brushes.Black;

        /// <summary>
        ///     New block stroke thickness
        /// </summary>
        public static int NewBlockStrokeThickness { get; } = 3;

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

        /// <summary>
        /// Default block quantity
        /// </summary>
        public static int DefaultBlockQuantity { get; } = 1;

        /// <summary>
        /// continuous simulation interval time in milliseconds
        /// </summary>
        public static int ContinuousSimulationInterval { get; } = 800;

        /// <summary>
        /// simulation background color
        /// Brushes.Beige was the old one
        /// </summary>
        public static Brush SimulationBackgroundColor { get; } = Brushes.Bisque;
        
        /// <summary>
        /// simulation border color
        /// </summary>
        public static Brush SimulationBorderColor { get; } = Brushes.Black;
    }
}