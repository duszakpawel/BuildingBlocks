using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic
{
    public class BlocksPreprocessor
    {
        private List<Block> blocks;
        private int canvasWidth;

        public BlocksPreprocessor(List<Block> blocks, int canvasWidth)
        {
            this.blocks = blocks;
            this.canvasWidth = canvasWidth;
        }

        public void Preprocess()
        {
            var maxBlockWidth = blocks.Max(item => item.Width);
            var maxBlockHeight = blocks.Max(item => item.Height);
            var maxEdgeLength = maxBlockHeight > maxBlockWidth ? maxBlockHeight : maxBlockWidth;
            var singleTileWidth = canvasWidth / (double)maxEdgeLength;
            blocks.ForEach(x => x.Preprocess(maxEdgeLength, (int)singleTileWidth));
        }
    }
}
