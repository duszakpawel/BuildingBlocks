using System.Collections.Generic;
using System.Linq;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Parsing
{
    /// <summary>
    ///     Blocks preprocessor class
    /// </summary>
    public class BlocksPreprocessor : IBlocksPreprocessor
    {
        /// <summary>
        /// Preprocesses list of blocks
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="canvasWidth"></param>
        /// <returns></returns>
        public List<Block> Preprocess(List<Block> blocks, int canvasWidth)
        {
            var maxBlockWidth = blocks.Max(item => item.Width);
            var maxBlockHeight = blocks.Max(item => item.Height);
            var maxEdgeLength = maxBlockHeight > maxBlockWidth ? maxBlockHeight : maxBlockWidth;
            var singleTileWidth = canvasWidth / (double) maxEdgeLength;
            blocks.ForEach(x => PreprocessSingle(x, maxEdgeLength, (int) singleTileWidth));
            return blocks;
        }

        private void PreprocessSingle(Block block, int maxEdgeLength, int singleTileWidth)
        {
            var rectVerticalShift = (int) ((double) (maxEdgeLength - block.Height)/2*singleTileWidth);
            for (var i = 0; i < block.Height; ++i)
            {
                for (var j = 0; j < block.Width; ++j)
                {
                    if (!block.Content[i, j])
                    {
                        continue;
                    }

                    var rectLeftPosition = j*singleTileWidth;
                    var topRectPosition = i*singleTileWidth + rectVerticalShift;
                    var rect = new RectItem
                    {
                        FillColor = Constants.BlockFillColor,
                        StrokeColor = Constants.BlockEdgeColor,
                        Width = singleTileWidth,
                        Height = singleTileWidth,
                        X = rectLeftPosition,
                        Y = topRectPosition
                    };

                    block.CanvasChildren.Add(rect);
                }
            }
        }
    }
}