using System.Collections.Generic;
using System.Linq;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Parsing
{
    /// <summary>
    ///     Blocks preprocessor class
    /// </summary>
    public class BlocksPreprocessor
    {
        private readonly List<Block> _blocks;
        private readonly int _canvasWidth;

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="blocks">blocks collection</param>
        /// <param name="canvasWidth">canvas width</param>
        public BlocksPreprocessor(List<Block> blocks, int canvasWidth)
        {
            _blocks = blocks;
            _canvasWidth = canvasWidth;
            Preprocess();
        }

        private void Preprocess()
        {
            var maxBlockWidth = _blocks.Max(item => item.Width);
            var maxBlockHeight = _blocks.Max(item => item.Height);
            var maxEdgeLength = maxBlockHeight > maxBlockWidth ? maxBlockHeight : maxBlockWidth;
            var singleTileWidth = _canvasWidth/(double) maxEdgeLength;
            _blocks.ForEach(x => PreprocessSingle(x, maxEdgeLength, (int) singleTileWidth));
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