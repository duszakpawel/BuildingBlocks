using BuildingBlocks.Models.Models;
using System.Collections.Generic;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    public interface IAlgorithmSolver
    {
        /// <summary>
        /// For each of simulations, executes one step and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="_k">k parameter</param>
        List<Simulation> Execute(IEnumerable<Simulation> simulations, int _k, int step);
    }
}