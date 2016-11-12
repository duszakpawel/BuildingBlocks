using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic
{
    public static class BlockLogic
    {
        public static IEnumerable<Block> RotateBlock(Block block)
        {
            yield return block;
        }

        public static Tuple<int, int> FindBestPlaceForBlock(bool[,] board, bool[,] block)
        {
            for (int j = board.GetLength(1) - 1; j >= 0; j--)
            {
                for (int i = 0; i <= board.GetLength(0); i++)
                {
                    if (CanBlockBePutInXy(board, block, i, j))
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            throw new Exception("Block cannot be put in any place on board");
        }

        private static bool CanBlockBePutInXy(bool[,] board, bool[,] block, int x, int y)
        {
            for (int i = 0; i < block.GetLength(0); i++)
            {
                for (int j = 0; j < block.GetLength(1); j++)
                {
                    if (block[i, j])
                    {
                        if (x + i >= board.GetLength(0) || y + j >= board.GetLength(1) || board[x + i, y + j])
                            return false;
                    }
                }
            }
            return true;
        }

    }
}
