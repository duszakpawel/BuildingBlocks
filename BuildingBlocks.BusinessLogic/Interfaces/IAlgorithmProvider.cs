using System.Collections.Generic;
using BuildingBlocks.Models.Models;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IAlgorithmProvider
    {
        /// <summary>
        ///     For each of simulations, executes one step and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        Task<List<Simulation>> Execute(IEnumerable<Simulation> simulations, int k, int step);
    }
}