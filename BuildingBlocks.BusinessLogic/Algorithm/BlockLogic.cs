using System;
using System.Collections.Generic;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    public static class BlockLogic
    {
        // TODO - zwrócić tutaj 4 obroty bloków (lub mniej jeżeli jest symetryczny w jakis sposób) 
        public static IEnumerable<Block> RotateBlock(Block block)
        {
            yield return block;
        }

        // Obecnie znajduje położenie (x,y) - lewy górny róg,  które jest najniżej jak się da. Z tego wiersza wybiera pozycję najbardziej po lewo. 
        // Do zmiany? Może czasami lepiej dać wyżej? Może zwracać IEnumerable<Tuple> i sprawdzać kilka? 
        public static Tuple<int, int> FindBestPlaceForBlock(bool[,] board, bool[,] block)
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

        private static bool CanBlockBePutInXy(bool[,] board, bool[,] block, int x, int y)
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
    }
}
