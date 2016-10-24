using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class Block : INotifyPropertyChanged
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool[,] Content { get; set; }

        public List<Rectangle> CanvasChildren { get; set; } = new List<Rectangle>();

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value >= 0)
                {
                    _quantity = value;
                }
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

        private int _quantity;

        private bool _isquantityenabled = true;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Preprocess(int maxEdgeLength, int singleTileWidth, Brush blockFillColor, Brush blockEdgeColor)
        {
            var rectVerticalShift = (int)((double)(maxEdgeLength - Height) / 2 * singleTileWidth);

            for (var i = 0; i < Height; ++i)
            {
                for (var j = 0; j < Width; ++j)
                {
                    if (!Content[i, j])
                    {
                        continue;
                    }

                    var rect = new Rectangle
                    {
                        Fill = blockFillColor,
                        Stroke = blockEdgeColor,
                        Width = singleTileWidth,
                        Height = singleTileWidth,
                    };

                    var rectLeftPosition = j * singleTileWidth;
                    var rightRectPosition = i * singleTileWidth + rectVerticalShift;

                    Canvas.SetTop(rect, rightRectPosition);
                    Canvas.SetLeft(rect, rectLeftPosition);

                    CanvasChildren.Add(rect);
                }
            }
        }
    }
}
