using BuildingBlocks.BusinessLogic.Interfaces;
using System.Linq;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    ///     Evaluate function provider
    /// </summary>
    public class EvaluateFunctionProvider : IEvaluateFunctionProvider
    {
        /// <summary>
        ///     Evaluation function. Returns score.        ///     
        /// </summary>
        /// <param name="content">content array</param>
        /// <returns>Score, which is:   (density (from 0 to 100)) minus (difference between heighest and lowest column) </returns>
        public int Evaluate(bool[,] content)
        {
            var simWidth = content.GetLength(0);
            int full = 0;
            int empty = 0;
            int maxColumnHeight = int.MinValue;
            int minColumnHeight = int.MaxValue;

            for (int i = 0; i < simWidth; i++)
            {
                var columnHeight = GetColumnMaxY(content, i);
                if (columnHeight > maxColumnHeight)
                    maxColumnHeight = columnHeight;
                if (columnHeight < minColumnHeight)
                    minColumnHeight = columnHeight;
                for (int j = content.GetLength(1) - 1; j >= columnHeight; j--)
                {
                    if (content[i, j])
                        full++;
                    else
                        empty++;
                }
            }

            return (100 * full / (empty + full)) - (maxColumnHeight - minColumnHeight);
        }

        private int GetColumnMaxY(bool[,] content, int col)
        {
            var simHeight = content.GetLength(1);

            for (var j = 0; j < simHeight; j++)
            {
                if (content[col, j])
                {
                    return j;
                }

            }

            return 0;
        }
    }
}