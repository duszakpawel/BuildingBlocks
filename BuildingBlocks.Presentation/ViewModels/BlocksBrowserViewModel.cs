using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class BlocksBrowserViewModel : Screen
    {
        public List<Block> DisplayedBlocks { get; set; }

        private const int CanvasWidth = 100;


        public BlocksBrowserViewModel(List<Block> displayedBlocks)
        {
            DisplayedBlocks = displayedBlocks;
            var maxWidth = displayedBlocks.Max(item => item.Width);
            var maxHeight = displayedBlocks.Max(item => item.Height);
            var max = maxHeight > maxWidth ? maxHeight : maxWidth;
            var rectSize = CanvasWidth / max;
            foreach (var block in DisplayedBlocks)
            {
                block.IsQuantityEnabled = true;
                block.CanvasChildren = new List<Rectangle>();
                // shift to center block vertically on list
                var rectVerticalShift = (int)((double)(max - block.Height) / 2 * rectSize);
                for (var i = 0; i < block.Height; ++i)
                {
                    for (var j = 0; j < block.Width; ++j)
                    {
                        if (!block.Content[i, j]) continue;
                        var rect = new Rectangle
                        {
                            Fill = Brushes.DeepSkyBlue,
                            Width = rectSize,
                            Height = rectSize,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetTop(rect, i * rectSize + rectVerticalShift);
                        Canvas.SetLeft(rect, j * rectSize);
                        block.CanvasChildren.Add(rect);
                    }
                }
            }
        }

        public void DisableQuantity()
        {
            foreach (var block in DisplayedBlocks)
            {
                block.IsQuantityEnabled = false;
            }
        }

        public void EnableQuantity()
        {
            foreach (var block in DisplayedBlocks)
            {
                block.IsQuantityEnabled = true;
            }
        }

        public void UpdateBrowserView(DisplayMode mode)
        {
            if (mode == DisplayMode.Selected)
            {
                LoadedBlocks = new List<Block>(DisplayedBlocks);
                DisplayedBlocks.RemoveAll(x => x.Quantity == 0);
            }
            else
            {
                DisplayedBlocks = LoadedBlocks;
            }
            NotifyOfPropertyChange(nameof(DisplayedBlocks));
        }

        public List<Block> LoadedBlocks { get; set; }
    }
}