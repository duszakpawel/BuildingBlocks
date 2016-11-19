using System.Collections.Generic;
using System.Linq;
using BuildingBlocks.Models;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;
using BuildingBlocks.BusinessLogic;

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    /// Blocks browser view model
    /// </summary>
    public class BlocksBrowserViewModel : Screen
    {
        /// <summary>
        /// Displayed (selected) blocks
        /// </summary>
        public List<Block> DisplayedBlocks { get; set; }

        /// <summary>
        /// Loaded blocks from file
        /// </summary>
        public List<Block> LoadedBlocks { get; set; }

        private const int CanvasWidth = 100;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="blocks">Blocks collection</param>
        public BlocksBrowserViewModel(List<Block> blocks)
        {
            PreprocessBlocks(blocks);
            DisplayedBlocks = blocks;
            LoadedBlocks = blocks;
        }

        /// <summary>
        /// Disables quantity control for each of loaded blocks
        /// </summary>
        public void DisableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = false);
        }

        /// <summary>
        /// Enables quantity control for each of loaded blocks
        /// </summary>
        public void EnableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = true);
        }

        /// <summary>
        /// Switches between selected blocks and loaded blocks in browser view
        /// </summary>
        /// <param name="mode">display view mode</param>
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

        private void PreprocessBlocks(List<Block> blocks)
        {
            var blocksPreprocessor = new BlocksPreprocessor(blocks, CanvasWidth);
            blocksPreprocessor.Preprocess();            
        }
    }
}