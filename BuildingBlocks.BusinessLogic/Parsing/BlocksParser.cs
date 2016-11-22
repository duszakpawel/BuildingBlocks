using System;
using System.IO;
using System.Threading.Tasks;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Models.Serializable;
using BuildingBlocks.BusinessLogic.Exceptions;
using BuildingBlocks.Models.Constants;

namespace BuildingBlocks.BusinessLogic.Parsing
{
    /// <summary>
    ///     Blocks parser class
    /// </summary>
    public class BlocksParser : IBlocksParser
    {
        private const char Separator = ' ';

        /// <summary>
        ///     Loads data from TextReader stream
        /// </summary>
        /// <param name="fileStream">file stream</param>
        /// <returns></returns>
        public async Task<GameData> LoadData(TextReader fileStream)
        {
            return await Task.Run(() =>
            {
                var gd = new GameData();

                var line = fileStream.ReadLine();
                if (line == null)
                {
                    return gd;
                }
                var parts = line.Split(Separator);

                int wellWidth;
                if (int.TryParse(parts[0], out wellWidth))
                {
                    gd.WellWidth = wellWidth;
                }
                else
                {
                    throw new ParsingException("Incorrect value of well width.");
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
                        throw new ParsingException("Incorrect value of blocks count.");
                    }
                }

                var counter = 1;
                try
                {
                    while ((line = fileStream.ReadLine()) != null)
                    {
                        parts = line.Split(Separator);

                        int width;
                        int height;

                        if (int.TryParse(parts[0], out width) == false)
                        {
                            throw new ParsingException($"Incorrect value of block width ({counter}).");
                        }

                        if (int.TryParse(parts[1], out height) == false)
                        {
                            throw new ParsingException($"Incorrect value of block height ({counter}).");
                        }

                        var block = new Block
                        {
                            Width = width,
                            Height = height,
                            Quantity = Constants.DefaultBlockQuantity,
                            IsQuantityEnabled = true
                        };

                        block.Content = new bool[block.Height, block.Width];

                        for (var i = 0; i < block.Height; ++i)
                        {
                            line = fileStream.ReadLine();
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
                                    block.Content[i, j] = val == 1;
                                }
                                else
                                {
                                    throw new ParsingException("Incorrect value of blocks count.");
                                }
                            }
                        }

                        gd.Blocks.Add(block);
                        counter++;
                    }
                }
                catch (Exception)
                {
                    throw new ParsingException();
                }

                return gd;
            });
        }
    }
}