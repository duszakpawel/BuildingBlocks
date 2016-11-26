using System.Collections.Generic;

namespace BuildingBlocks.BusinessLogic.Extension_methods
{
    /// <summary>
    ///     Array extension methods
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Compares content of two two-dimensional arrays and returns information whether the content is equal or not.
        /// </summary>
        /// <typeparam name="T">Type of element which is stored in arrays</typeparam>
        /// <param name="a1">First array</param>
        /// <param name="a2">Second array</param>
        /// <returns></returns>
        public static bool HasEqualContent<T>(this T[,] a1, T[,] a2)
        {
            if (ReferenceEquals(a1, a2))
            {
                return true;
            }

            if (a1 == null || a2 == null)
            {
                return false;
            }

            if (a1.GetLength(0) != a2.GetLength(0))
            {
                return false;
            }

            if (a1.GetLength(1) != a2.GetLength(1))
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;

            for (var i = 0; i < a1.GetLength(0); i++)
            {
                for (var j = 0; j < a1.GetLength(1); j++)
                {
                    if (comparer.Equals(a1[i, j], a2[i, j]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}