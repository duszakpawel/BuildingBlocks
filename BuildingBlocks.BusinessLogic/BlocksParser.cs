using BuildingBlocks.Models;
using System;
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

                var line = file.ReadLine();
                if (line == null)
                {
                    return gd;
                }
                var parts = line.Split(Separator);

                int wellWidth;
                if(int.TryParse(parts[0], out wellWidth))
                {
                    gd.WellWidth = wellWidth;
                }
                else
                {
                    throw new ArgumentException("Incorrect value of well width.");
                }

                if (parts.Length > 1)
                {
                    int blocksClount;
                    if (int.TryParse(parts[1], out blocksClount))
                    {
                        gd.BlocksCount = blocksClount;
                    }
                    else
                    {
                        throw new ArgumentException("Incorrect value of blocks count.");
                    }
                }

                var counter = 1;
                while ((line = file.ReadLine()) != null)
                {
                    parts = line.Split(Separator);

                    int width;
                    int height;

                    if (int.TryParse(parts[0], out width) == false)
                    {                    
                        throw new ArgumentException($"Incorrect value of block width ({counter}).");
                    }

                    if (int.TryParse(parts[1], out height) == false)
                    {                     
                        throw new ArgumentException($"Incorrect value of block height ({counter}).");
                    }

                    var block = new Block
                    {
                        Width = width,
                        Height = height,
                        Quantity = 0,
                        IsQuantityEnabled = true
                    };

                    block.Content = new bool[block.Height, block.Width];

                    for (var i = 0; i < block.Height; ++i)
                    {
                        line = file.ReadLine();
                        if (line == null)
                        {
                            continue;
                        }
                        parts = line.Split(Separator);
                        for (var j = 0; j < block.Width; ++j)
                        {
                            int val;
                            if (int.TryParse(parts[j], out val))
                            {
                                block.Content[i, j] = (val == 1);
                            }
                            else
                            {
                                throw new ArgumentException("Incorrect value of blocks count.");
                            }
                        }
                    }

                    gd.Blocks.Add(block);
                    counter++;
                }
                return gd;
            });
        }
    }
}
