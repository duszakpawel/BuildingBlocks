using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic
{
    public class SimulationEqualityComparer : IEqualityComparer<KeyValuePair<Simulation, int>>
    {
        public bool Equals(KeyValuePair<Simulation, int> x, KeyValuePair<Simulation, int> y)
        {
            var contentEquality = x.Key.Content.HasEqualContent(y.Key.Content);
            var lastBlockEquality = x.Key.LastBlock.HasEqualContent(y.Key.LastBlock);
            var information = contentEquality && lastBlockEquality;

            return information;
        }

        public int GetHashCode(KeyValuePair<Simulation, int> obj)
        {
            var array = obj.Key.Content;
            var constValue = 314159;
            int hc = array.Length;
            foreach (var element in array)
            {
                // not very clever but for this purpose should be enough
                hc = unchecked(hc * constValue + (element ? 1 : 0));
            }
            return hc;
        }
    }
}
