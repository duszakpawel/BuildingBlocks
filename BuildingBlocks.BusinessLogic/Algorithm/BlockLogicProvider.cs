using System;
using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    /// Block logic class provider
    /// </summary>
    public class BlockLogicProvider : IBlockLogicProvider
    {
        /// <summary>
        /// Returns collection of rotated block
        /// TODO: zwrócić tutaj 4 obroty bloków (lub mniej jeżeli jest symetryczny w jakis sposób) 
        /// </summary>
        /// <param name="block">Block conntent</param>
        /// <returns>collection of rotated block</returns>
        public IList<Block> RotateBlock(Block block)
        {
            var ret = new List<Block> { new Block(block) };
            for (var i = 1; i < 4; ++i)
            {
                ret.Add(new Block
                {
                    Height = ret[i - 1].Width,
                    Width = ret[i - 1].Height,
                    Content = RotateMatrixClockwise(ret[i - 1].Content),
                    IsQuantityEnabled = block.IsQuantityEnabled,
                    Quantity = block.Quantity,
                    Id = block.Id
                });
            }

            return ret;
        }

        /// <summary>
        /// Obecnie znajduje położenie (x,y) - lewy górny róg,  które jest najniżej jak się da. Z tego wiersza wybiera pozycję najbardziej po lewo. 
        /// TODO: Do zmiany? Może czasami lepiej dać wyżej? Może zwracać IEnumerable<Tuple> i sprawdzać kilka? 
        /// </summary>
        /// <param name="board">Board content</param>
        /// <param name="block">Block conntent</param>
        /// <returns>Best position (x,y) for block</returns>
        public Tuple<int, int> FindBestPlaceForBlock(bool[,] board, bool[,] block)
        {
            for (var j = board.GetLength(1) - 1; j >= 0; j--)
            {
                for (var i = 0; i <= board.GetLength(0); i++)
                {
                    if (CanBlockBePutInXy(board, block, i, j))
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            throw new Exception("Block cannot be put in any place on board");
        }

        private bool CanBlockBePutInXy(bool[,] board, bool[,] block, int x, int y)
        {
            for (var i = 0; i < block.GetLength(0); i++)
            {
                for (var j = 0; j < block.GetLength(1); j++)
                {
                    if (!block[i, j]) continue;
                    if (x + i >= board.GetLength(0) || y + j >= board.GetLength(1) || board[x + i, y + j])
                        return false;
                }
            }
            return true;
        }

        private bool[,] RotateMatrixClockwise(bool[,] oldMatrix)
        {
            var newMatrix = new bool[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            var newRow = 0;
            for (var oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                var newColumn = 0;
                for (var oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }
    }
}
