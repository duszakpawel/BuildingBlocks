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
        private const char separator = ' ';

        public GameData LoadData(TextReader file)
        {
            GameData gd = new GameData();
            string line;

            // first line
            line = file.ReadLine();
            var parts = line.Split(separator);
            gd.WellWidth = int.Parse(parts[0]);
            gd.BlocksCount = int.Parse(parts[1]);
            gd.Blocks = new List<Block>();
            
            // next lines
            while ((line = file.ReadLine()) != null)
            {
                parts = line.Split(separator);
                Block b = new Block();
                b.Width = int.Parse(parts[0]);
                b.Height = int.Parse(parts[1]);
                b.Content = new bool[b.Height,b.Width];
                for (int i = 0; i < b.Height; i++)
                {
                    line = file.ReadLine();
                    parts = line.Split(separator);
                    for (int j = 0; j < b.Width; j++)
                    {
                        int val = int.Parse(parts[j]);
                        b.Content[i,j] = (val == 1);
                    }
                }
                gd.Blocks.Add(b);
            }
            return gd;
        }
    }
}
