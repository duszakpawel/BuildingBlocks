using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    ///     Algorithm solver
    /// </summary>
    public class AlgorithmSolver : IAlgorithmSolver
    {
        private readonly IBlockLogicProvider _blockLogicProvider;
        private readonly IEvaluateFunctionProvider _evaluateFunctionProvider;
        private bool _computationsTerminated;
        private int _k;
        private IEnumerable<Simulation> _simulations;

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="evaluateFunctionProvider"></param>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        /// <param name="blockLogicProvider"></param>
        public AlgorithmSolver(IBlockLogicProvider blockLogicProvider,
            IEvaluateFunctionProvider evaluateFunctionProvider, IEnumerable<Simulation> simulations, int k)
        {
            _blockLogicProvider = blockLogicProvider;
            _evaluateFunctionProvider = evaluateFunctionProvider;
            _simulations = simulations;
            _k = k;
        }

        /// <summary>
        ///     For each of simulations, executes one step and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<Simulation> Execute(IEnumerable<Simulation> simulations, int k, int step)
        {
            foreach (var simulation in simulations)
            {
                simulation.LastBlock = new int[simulation.Content.GetLength(0), simulation.Content.GetLength(1)];
            }
            var ret = new List<Simulation>();

            for (var i = 0; i < step; i++)
            {
                if (_computationsTerminated)
                {
                    break;
                }

                var dict = new Dictionary<Simulation, int>();
                foreach (var simulation in simulations)
                {
                    simulation.AvailableBlocks.RemoveAll(b => b.Quantity <= 0);
                    CheckAndCorrectSimulationHeight(simulation);
                    foreach (var block in simulation.AvailableBlocks)
                    {
                        foreach (var b in _blockLogicProvider.RotateBlock(block))
                        {
                            foreach (var xy in _blockLogicProvider.FindBestPlacesForBlock(simulation.Content, b.Content))
                            {
                                var sim = AddBlockToSimulation(b, simulation, xy.Item1, xy.Item2);
                                var score = _evaluateFunctionProvider.Evaluate(sim.Content);
                                sim.Score = score;
                                sim.Height = GetSimulationHeight(sim.Content);
                                dict.Add(sim, score);
                            }
                        }
                    }
                }
                dict = dict.Distinct(new SimulationEqualityComparer()).ToDictionary(x => x.Key, x => x.Value);
                var bestScores = dict.Values.OrderByDescending(v => v).Take(k).Distinct().ToList();
                ret = dict.Where(d => bestScores.Contains(d.Value)).Select(p => p.Key).Take(k).ToList();
                simulations = ret;

                if (simulations.FirstOrDefault()?.AvailableBlocks.Count == 0)
                {
                    _computationsTerminated = true;
                    break;
                }
            }

            foreach (var sim in ret)
            {
                SyncCanvasWithContent(sim);
            }

            return ret;
        }

        /// <summary>
        ///     Adds new block to simulation. x and y are coordinates of top left corner of block
        /// </summary>
        /// <param name="block">Block to add</param>
        /// <param name="simulation">Simulation object</param>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        /// <returns></returns>
        private Simulation AddBlockToSimulation(Block block, Simulation simulation, int x, int y)
        {
            var list = new List<Block>();
            foreach (var b in simulation.AvailableBlocks)
            {
                list.Add(new Block(b));
            }
            var sim = new Simulation
            {
                Content = (bool[,])simulation.Content.Clone(),
                AvailableBlocks = list,
                WellHeight = simulation.WellHeight,
                LastBlock = (int[,])simulation.LastBlock.Clone()
            };

            var bl = sim.AvailableBlocks.Single(b => b.Id == block.Id);
            bl.Quantity--;
            if (bl.Quantity == 0)
            {
                sim.AvailableBlocks.Remove(bl);
            }

            var lastBlockCurrentId = sim.LastBlock.Cast<int>().Max();
            for (var i = 0; i < block.Height; i++)
            {
                for (var j = 0; j < block.Width; j++)
                {
                    if (!block.Content[i, j])
                    {
                        continue;
                    }

                    if (sim.Content[x + i, y + j])
                    {
                        throw new ArgumentException("This place in simulation is already filled");
                    }

                    sim.Content[x + i, y + j] = true;
                    sim.LastBlock[x + i, y + j] = lastBlockCurrentId + 1;
                }
            }

            return sim;
        }

        /// <summary>
        ///     Updates colors of blocks and produces new rectangle items collection to be displayed
        /// </summary>
        /// <param name="simulation">simulation object</param>
        private void SyncCanvasWithContent(Simulation simulation)
        {
            var children = new List<RectItem>();
            for (var i = 0; i < simulation.Content.GetLength(0); i++)
            {
                for (var j = 0; j < simulation.Content.GetLength(1); j++)
                {
                    if (simulation.LastBlock[i, j] > 0)
                    {
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth)
                        {
                            FillColor = Constants.FillBrushes[simulation.LastBlock[i, j] % Constants.FillBrushes.Count]
                        });
                    }
                    else if (simulation.Content[i, j])
                    {
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth));
                    }
                }
            }

            simulation.CanvasChildren = new ObservableCollection<RectItem>(children);
        }

        /// <summary>
        ///     corrects simulation height
        /// </summary>
        /// <param name="simulation">simulation object</param>
        private void CheckAndCorrectSimulationHeight(Simulation simulation)
        {
            for (var j = 0; j < Constants.CompulsoryFreeSpaceInWellHeight; j++)
            {
                var free = true;
                for (var i = 0; i < simulation.Content.GetLength(0); i++)
                {
                    if (!simulation.Content[i, j])
                    {
                        continue;
                    }

                    free = false;
                    break;
                }

                if (free)
                {
                    continue;
                }
                // make well bigger: 
                simulation.WellHeight += Constants.CompulsoryFreeSpaceInWellHeight * Constants.SingleTileWidth;
                var newContent =
                    new bool[simulation.Content.GetLength(0),
                        simulation.Content.GetLength(1) + Constants.CompulsoryFreeSpaceInWellHeight];
                var newLastBlocks =
                    new int[simulation.Content.GetLength(0),
                        simulation.Content.GetLength(1) + Constants.CompulsoryFreeSpaceInWellHeight];

                for (var i = 0; i < simulation.Content.GetLength(0); i++)
                {
                    for (var k = 0; k < simulation.Content.GetLength(1); k++)
                    {
                        newContent[i, k + Constants.CompulsoryFreeSpaceInWellHeight] = simulation.Content[i, k];
                        newLastBlocks[i, k + Constants.CompulsoryFreeSpaceInWellHeight] = simulation.LastBlock[i, k];
                    }
                }

                simulation.Content = newContent;
                simulation.LastBlock = newLastBlocks;

                return;
            }
        }

        private int GetSimulationHeight(bool[,] content)
        {
            var height = content.GetLength(1);
            var width = content.GetLength(0);

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    if (content[i, j])
                    {
                        return height - j;
                    }
                }
            }

            return 0;
        }
    }
}