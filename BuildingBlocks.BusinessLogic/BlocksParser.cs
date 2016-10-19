using BuildingBlocks.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic
{
    public class BlocksParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Blocks and quantities of them</returns>
        public Dictionary<Block,int> LoadBlocks(string fileName)
        {
            Dictionary<Block,int> ret = new Dictionary<Block, int>();
            string line;
            StreamReader file =new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
            }
            return ret;
        }
    }
}
