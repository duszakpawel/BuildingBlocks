using System.Collections.Generic;
using BuildingBlocks.BusinessLogic.Extension_methods;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    ///     Simulation equality provider class
    /// </summary>
    public class SimulationEqualityComparer : IEqualityComparer<KeyValuePair<Simulation, int>>
    {
        /// <summary>
        ///     Returns information whether two simulations are equal or not
        /// </summary>
        /// <param name="x">first simulation</param>
        /// <param name="y">second simulation</param>
        /// <returns></returns>
        public bool Equals(KeyValuePair<Simulation, int> x, KeyValuePair<Simulation, int> y)
        {
            var contentEquality = x.Key.Content.HasEqualContent(y.Key.Content);
            var lastBlockEquality = x.Key.LastBlock.HasEqualContent(y.Key.LastBlock);
            var information = contentEquality && lastBlockEquality;

            return information;
        }

        /// <summary>
        ///     GetHashCode method required by IEqualityComparer interface
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(KeyValuePair<Simulation, int> obj)
        {
            var array = obj.Key.Content;
            const int constValue = 314159;
            var hc = array.Length;
            foreach (var element in array)
            {
                // not very clever but for this purpose should be enough
                hc = unchecked(hc*constValue + (element.GetHashCode()));
            }

            return hc;
        }
    }
}