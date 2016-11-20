using System.IO;
using System.Threading.Tasks;
using BuildingBlocks.Models.Serializable;

namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IBlocksParser
    {
        /// <summary>
        ///     Loads data from TextReader stream
        /// </summary>
        /// <param name="fileStream">file stream</param>
        Task<GameData> LoadData(TextReader fileStream);
    }
}