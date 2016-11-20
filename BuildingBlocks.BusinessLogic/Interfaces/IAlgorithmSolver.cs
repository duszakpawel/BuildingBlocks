using System.Collections.Generic;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IAlgorithmSolver
    {
        /// <summary>
        ///     For each of simulations, executes one step and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        List<Simulation> Execute(IEnumerable<Simulation> simulations, int k, int step);
    }
}