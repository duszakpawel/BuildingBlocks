using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using BuildingBlocks.Models.Properties;

namespace BuildingBlocks.Models.Models
{
    /// <summary>
    ///     Rectangle item class
    /// </summary>
    public class RectItem : INotifyPropertyChanged
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public RectItem()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        public RectItem(int x, int y)
        {
            X = x;
            Y = y;
            Height = Constants.Constants.SingleTileWidth;
            Width = Constants.Constants.SingleTileWidth;
            FillColor = Constants.Constants.OldBlockFillColor;
            StrokeColor = Constants.Constants.BlockEdgeColor;
            StrokeThickness = 1;
        }

        /// <summary>
        ///     X- coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Y - coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///     Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Fill color
        /// </summary>
        public Brush FillColor { get; set; }

        /// <summary>
        ///     Stroke color
        /// </summary>
        public Brush StrokeColor { get; set; }

        /// <summary>
        ///     Stroke thickness
        /// </summary>
        public int StrokeThickness { get; set; }

        /// <summary>
        ///     Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Property changed event handler
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}