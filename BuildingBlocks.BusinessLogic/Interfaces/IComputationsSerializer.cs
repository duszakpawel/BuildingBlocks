using System;
using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IComputationsSerializer
    {
        /// <summary>
        ///     Stores computations data in XML file with specified name.
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <param name="boardWidth">Board width</param>
        /// <param name="K">K parameter</param>
        /// <param name="simulations">Simulations collection</param>
        void Serialize(string filename, int boardWidth, int K, IEnumerable<Simulation> simulations);
        /// <summary>
        ///     Deserializes computations data from specified XML file.
        /// </summary>
        /// <param name="filename">Name of XML file</param>
        /// <returns>BoardWidth, K, Simulations</returns>
        Tuple<int, int, IEnumerable<Simulation>> Deserialize(string filename);
    }
}