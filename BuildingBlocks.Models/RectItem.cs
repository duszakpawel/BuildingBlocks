using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class RectItem : INotifyPropertyChanged
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
            FillColor = Constants.OldBlockEdgeColor;
            StrokeColor = Constants.BlockEdgeColor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}