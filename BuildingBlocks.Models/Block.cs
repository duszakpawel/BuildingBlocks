using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class Block : INotifyPropertyChanged
    {
        private static int idCounter = 0;

        public int Id;
        public int Width { get; set; }

        public int Height { get; set; }

        public bool[,] Content { get; set; }

        public List<RectItem> CanvasChildren { get; set; } = new List<RectItem>();

        public int Quantity { get; set; }

        public bool IsQuantityEnabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Block()
        {
            Id = idCounter++;
        }

        public Block(Block block)
        {
            Id = block.Id;
            Width = block.Width;
            Height = block.Height;
            Content = (bool[,]) block.Content.Clone();
            Quantity = block.Quantity;
        }

        public void Preprocess(int maxEdgeLength, int singleTileWidth)
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
                    var rectLeftPosition = j * singleTileWidth;
                    var topRectPosition = i * singleTileWidth + rectVerticalShift;
                    var rect = new RectItem
                    {
                        FillColor = Constants.BlockFillColor,
                        StrokeColor = Constants.BlockEdgeColor,
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
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
