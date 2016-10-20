﻿using BuildingBlocks.Models;
using System.Collections.Generic;
using System.IO;

namespace BuildingBlocks.BusinessLogic
{
    public class BlocksParser
    {
        private const char Separator = ' ';

        public GameData LoadData(TextReader file)
        {
            var gd = new GameData();

            // first line
            var line = file.ReadLine();
            if (line == null) return gd;
            var parts = line.Split(Separator);
            gd.WellWidth = int.Parse(parts[0]);
            gd.BlocksCount = int.Parse(parts[1]);
            gd.Blocks = new List<Block>();

            // next lines
            while ((line = file.ReadLine()) != null)
            {
                parts = line.Split(Separator);
                var b = new Block
                {
                    Width = int.Parse(parts[0]),
                    Heigth = int.Parse(parts[1])
                };
                b.Content = new bool[b.Heigth, b.Width];
                for (var i = 0; i < b.Heigth; ++i)
                {
                    line = file.ReadLine();
                    if (line == null) continue;
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
        }
    }
}
