using System;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BuildingBlocks.BusinessLogic.Exceptions;

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
        /// <param name="board">board array</param>
        /// <returns>Score</returns>
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public int Evaluate(int[,] board)
        {
            if (board == null)
            {
                throw new EvaluateFunctionException("Board provided to evaluate function was null.");
            }

            var simWidth = board.GetLength(0);
            var full = 0;
            var empty = 0;
            var maxColumnHeight = int.MinValue;
            var minColumnHeight = int.MaxValue;

            var columnHeights = new Dictionary<int, int>();

            for (var i = 0; i < simWidth; i++)
            {
                var columnHeight = GetColumnMaxY(board, i);

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
            if (lowerBound > board.GetLength(1) - 1)
                lowerBound = board.GetLength(1) - 1;

            for (var i = 0; i < simWidth; i++)
            {
                var columnHeight = columnHeights[i];
                for (var j = lowerBound; j >= columnHeight; j--)
                {
                    if (board[i, j] > 0)
                    {
                        full++;
                    }
                    else
                    {
                        empty++;
                    }
                }
            }

            try
            {
                return
                    (int)
                        ((100*full/(empty + full)) -
                         (maxColumnHeight - minColumnHeight)*Constants.ColumnHeightDifferenceMultiplier);
            }
            catch (DivideByZeroException)
            {
                return 100;
            }
        }

        private static int GetColumnMaxY(int[,] content, int col)
        {
            var simHeight = content.GetLength(1);

            for (var j = 0; j < simHeight; j++)
            {
                if (content[col, j] > 0)
                {
                    return j;
                }

            }

            return simHeight;
        }
    }
}