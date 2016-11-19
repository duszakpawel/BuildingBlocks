using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Models
{
    public static class ArrayExtensions
    {
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

            if (a1.Length != a2.Length)
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;

            for(int i=0; i< a1.GetLength(0); i++)
            {
                for (int j = 0; j < a1.GetLength(1); j++)
                {
                    if (comparer.Equals(a1[i,j], a2[i,j]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
