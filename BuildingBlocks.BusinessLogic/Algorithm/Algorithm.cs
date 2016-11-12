using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    public static class Algorithm
    {
        public static ObservableCollection<Simulation> Execute(ObservableCollection<Simulation> simulations, int k)
        {
            var dict = new Dictionary<Simulation, int>();
            foreach (var simulation in simulations)
            {
                CheckAndCorrectSimulationHeight(simulation);
                foreach (var block in simulation.AvailableBlocks)
                {
                    foreach (var b in BlockLogic.RotateBlock(block))
                    {
                        var xy = BlockLogic.FindBestPlaceForBlock(simulation.Content, b.Content);
                        var sim = AddBlockToSimulation(block, simulation, xy.Item1, xy.Item2);
                        var score = EvaluateFunction.Evaluate(sim.Content);
                        dict.Add(sim, score);
                    }
                }
            }

            var bestScores = dict.Values.OrderByDescending(v => v).Take(k).Distinct().ToList();
            var ret = new ObservableCollection<Simulation>(dict.Where(d => bestScores.Contains(d.Value)).Select(p => p.Key).Take(k));

            foreach (var sim in ret)
            {
                SyncCanvasWithContent(sim);
            }

            return ret;
        }

        // x and y are coordinates of top left corner of block
        private static Simulation AddBlockToSimulation(Block block, Simulation simulation, int x, int y)
        {
            var sim = new Simulation()
            {
                Content = (bool[,])simulation.Content.Clone(),
                AvailableBlocks = new List<Block>(simulation.AvailableBlocks),
                WellHeight = simulation.WellHeight,
                LastBlock = new bool[simulation.Content.GetLength(0), simulation.Content.GetLength(1)]
            };

            sim.AvailableBlocks.Remove(block);

            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Content[i, j])
                    {
                        if (sim.Content[x + i, y + j])
                            throw new ArgumentException("This place in simulation is already filled");
                        sim.Content[x + i, y + j] = true;
                        sim.LastBlock[x + i, y + j] = true;
                    }
                }
            }
            return sim;
        }

        private static void SyncCanvasWithContent(Simulation simulation)
        {
            var children = new ObservableCollection<RectItem>();
            for (int i = 0; i < simulation.Content.GetLength(0); i++)
            {
                for (int j = 0; j < simulation.Content.GetLength(1); j++)
                {
                    if (simulation.LastBlock[i, j])
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth)
                        {
                            FillColor = Constants.BlockFillColor
                        });
                    else
                        if (simulation.Content[i, j])
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth));

                }
            }
            simulation.CanvasChildren = children;
        }

        private static void CheckAndCorrectSimulationHeight(Simulation simulation)
        {
            for (int j = 0; j < Constants.CompulsoryFreeSpaceInWellHeight; j++)
            {
                bool free = true;
                for (int i = 0; i < simulation.Content.GetLength(0); i++)
                {
                    if (simulation.Content[i,j])
                    {
                        free = false;
                        break;
                    }
                }
                if (!free)
                {
                    // make well bigger: 
                    simulation.WellHeight += Constants.CompulsoryFreeSpaceInWellHeight*Constants.SingleTileWidth;
                    bool[,] newContent = new bool[simulation.Content.GetLength(0), simulation.Content.GetLength(1) + Constants.CompulsoryFreeSpaceInWellHeight];
                    for (int ii = 0; ii < simulation.Content.GetLength(0); ii++)
                    {
                        for (int jj = 0; jj < simulation.Content.GetLength(1); jj++)
                        {
                            newContent[ii, jj + Constants.CompulsoryFreeSpaceInWellHeight] = simulation.Content[ii, jj];
                        }
                    }
                    simulation.Content = newContent;
                    return;
                }
            }
        }
    }
}
