using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class RectItem
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public Brush FillColor { get; set; }

        public Brush StrokeColor { get; set; }
    }

    public class Block : INotifyPropertyChanged
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool[,] Content { get; set; }

        public List<RectItem> CanvasChildren { get; set; } = new List<RectItem>();

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public bool IsQuantityEnabled
        {
            get
            {
                return _isquantityenabled;
            }
            set
            {
                _isquantityenabled = value;
                OnPropertyChanged(nameof(IsQuantityEnabled));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static double SingleTileWidth { get; set; }

        private int _quantity;

        private bool _isquantityenabled = true;

        public void Preprocess(int maxEdgeLength, double singleTileWidth, Brush blockFillColor, Brush blockEdgeColor)
        {
            SingleTileWidth = singleTileWidth;
            var rectVerticalShift = (int)((double)(maxEdgeLength - Height) / 2 * singleTileWidth);
            for (var i = 0; i < Height; ++i)
            {
                for (var j = 0; j < Width; ++j)
                {
                    if (!Content[i, j])
                    {
                        continue;
                    }
                    var rectLeftPosition = j * singleTileWidth;
                    var topRectPosition = i * singleTileWidth + rectVerticalShift;
                    var rect = new RectItem
                    {
                        FillColor = blockFillColor,
                        StrokeColor = blockEdgeColor,
                        Width = singleTileWidth,
                        Height = singleTileWidth,
                        X = rectLeftPosition,
                        Y = topRectPosition
                    };
                    CanvasChildren.Add(rect);
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
