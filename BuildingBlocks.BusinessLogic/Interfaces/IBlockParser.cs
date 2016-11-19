using BuildingBlocks.Models;
using System.IO;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic
{
    public interface IBlocksParser
    {
        /// <summary>
        /// Loads data from TextReader stream
        /// </summary>
        /// <param name="fileStream">file stream</param>
        Task<GameData> LoadData(TextReader fileStream);
    }
}