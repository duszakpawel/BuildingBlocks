using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using System.Collections.Generic;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    ///     Evaluate function provider
    /// </summary>
    public class EvaluateFunctionProvider : IEvaluateFunctionProvider
    {
        /// <summary>
        ///     Evaluation function. Score is: 
        ///         (density (from 0 to 100) in area which bottom line is lowest column minus constant,
        ///         upper line in each column is different and it is heighest filled cell) 
        ///             minus 
        ///         (difference between heighest and lowest column multiplied by constant) 
        /// </summary>
        /// <param name="content">content array</param>
        /// <returns>Score</returns>
        public int Evaluate(int[,] content)
        {
            var simWidth = content.GetLength(0);
            int full = 0;
            int empty = 0;
            int maxColumnHeight = int.MinValue;
            int minColumnHeight = int.MaxValue;

            var columnHeights = new Dictionary<int, int>();

            for (int i = 0; i < simWidth; i++)
            {
                var columnHeight = GetColumnMaxY(content, i);

                if (columnHeight > maxColumnHeight)
                {
                    maxColumnHeight = columnHeight;
                }

                if (columnHeight < minColumnHeight)
                {
                    minColumnHeight = columnHeight;
                }

                columnHeights.Add(i, columnHeight);
            }

            var lowerBound = maxColumnHeight + Constants.HeightForCountingDensity;
            if (lowerBound > content.GetLength(1) - 1)
                lowerBound = content.GetLength(1) - 1;

            for (int i = 0; i < simWidth; i++)
            {
                var columnHeight = columnHeights[i];
                for (int j = lowerBound; j >= columnHeight; j--)
                {
                    if (content[i, j] > 0)
                    {
                        full++;
                    }
                    else
                    {
                        empty++;
                    }
                }
            }

            return (int)((100 * full / (empty + full)) - ((maxColumnHeight - minColumnHeight) * Constants.ColumnHeightDifferenceMultiplier));
        }

        private int GetColumnMaxY(int[,] content, int col)
        {
            var simHeight = content.GetLength(1);

            for (var j = 0; j < simHeight; j++)
            {
                if (content[col, j] > 0)
                {
                    return j;
                }

            }

            return simHeight - 1;
        }
    }
}