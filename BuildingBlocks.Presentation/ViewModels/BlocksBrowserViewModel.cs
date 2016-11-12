using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using BuildingBlocks.Models;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class BlocksBrowserViewModel : Screen
    {
        public List<Block> DisplayedBlocks { get; set; }
        public List<Block> LoadedBlocks { get; set; }
        private const int CanvasWidth = 100;

        public BlocksBrowserViewModel(List<Block> blocks)
        {
            PreprocessBlocks(blocks);
            DisplayedBlocks = blocks;
            LoadedBlocks = blocks;
        }

        public void DisableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = false);
        }

        public void EnableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = true);
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

        private static void PreprocessBlocks(List<Block> blocks)
        {
            var maxBlockWidth = blocks.Max(item => item.Width);
            var maxBlockHeight = blocks.Max(item => item.Height);
            var maxEdgeLength = maxBlockHeight > maxBlockWidth ? maxBlockHeight : maxBlockWidth;
            var singleTileWidth = CanvasWidth / (double)maxEdgeLength;
            blocks.ForEach(x => x.Preprocess(maxEdgeLength, (int) singleTileWidth));
        }
    }
}