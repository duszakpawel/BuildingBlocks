using BuildingBlocks.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic
{
    public class BlocksParser
    {
        private const char Separator = ' ';

        public async Task<GameData> LoadData(TextReader file)
        {
            return await Task.Run(() =>
            {
                var gd = new GameData();
                // first line
                var line = file.ReadLine();
                if (line == null)
                {
                    return gd;
                }
                var parts = line.Split(Separator);
                gd.WellWidth = int.Parse(parts[0]);
                if (parts.Length > 1)
                    gd.BlocksCount = int.Parse(parts[1]);
                gd.Blocks = new List<Block>();
                // next lines
                while ((line = file.ReadLine()) != null)
                {
                    parts = line.Split(Separator);
                    var b = new Block
                    {
                        Width = int.Parse(parts[0]),
                        Height = int.Parse(parts[1]),
                        Quantity = 1,
                        IsQuantityEnabled = true
                    };
                    b.Content = new bool[b.Height, b.Width];
                    for (var i = 0; i < b.Height; ++i)
                    {
                        line = file.ReadLine();
                        if (line == null)
                        {
                            continue;
                        }
                        parts = line.Split(Separator);
                        for (var j = 0; j < b.Width; ++j)
                        {
                            var val = int.Parse(parts[j]);
                            b.Content[i, j] = (val == 1);
                        }
                    }
                    gd.Blocks.Add(b);
                }

                return gd;
            });
        }
    }
}
