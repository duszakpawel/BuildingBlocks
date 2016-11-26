using System.Collections.Generic;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    ///     Blocks browser view model
    /// </summary>
    public class BlocksBrowserViewModel : Screen
    {
        private const int CanvasWidth = 100;
        private readonly IBlocksPreprocessor _blocksPreprocessor;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="blocksPreprocessor"></param>
        /// <param name="blocks">Blocks collection</param>
        public BlocksBrowserViewModel(IBlocksPreprocessor blocksPreprocessor, List<Block> blocks)
        {
            _blocksPreprocessor = blocksPreprocessor;
            if (blocks?.Count > 0)
            {
                PreprocessBlocks(blocks);
            }
            DisplayedBlocks = blocks;
            LoadedBlocks = blocks;
        }

        /// <summary>
        ///     Displayed (selected) blocks
        /// </summary>
        public List<Block> DisplayedBlocks { get; private set; }

        /// <summary>
        ///     Loaded blocks from file
        /// </summary>
        public List<Block> LoadedBlocks { get; private set; }

        /// <summary>
        ///     Disables quantity control for each of loaded blocks
        /// </summary>
        public void DisableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = false);
        }

        /// <summary>
        ///     Enables quantity control for each of loaded blocks
        /// </summary>
        public void EnableQuantity()
        {
            LoadedBlocks.ForEach(x => x.IsQuantityEnabled = true);
        }

        /// <summary>
        ///     Switches between selected blocks and loaded blocks in browser view
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
            _blocksPreprocessor.Preprocess(blocks, CanvasWidth);
        }

        public void SetQuantityOfEveryBlock(int quantityOfEveryBlock)
        {
            foreach (var element in LoadedBlocks)
            {
                element.Quantity = quantityOfEveryBlock;
            }
        }
    }
}