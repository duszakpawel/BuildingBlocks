using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic
{
    public class EvaluateFunction
    {
        // for now best score is biggest score - you may change it 
        public static int Evaluate(bool[,] content)
        {
            int height = 0;
            for (int j = 0; j < content.GetLength(1); j++)
            {
                for (int i = 0; i < content.GetLength(0); i++)
                {
                    if (content[i, j])
                        return j;
                }
            }
            return height;
        }
    }
}
