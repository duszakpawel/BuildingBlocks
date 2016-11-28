using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;
using BuildingBlocks.BusinessLogic.Exceptions;
using System.Threading.Tasks;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    ///     Algorithm provider
    /// </summary>
    public class AlgorithmProvider : IAlgorithmProvider
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
        /// <param name="blockLogicProvider"></param>
        public AlgorithmProvider(IBlockLogicProvider blockLogicProvider, IEvaluateFunctionProvider evaluateFunctionProvider)
        {
            _blockLogicProvider = blockLogicProvider;
            _evaluateFunctionProvider = evaluateFunctionProvider;
        }

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="evaluateFunctionProvider"></param>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        /// <param name="blockLogicProvider"></param>
        public AlgorithmProvider(IBlockLogicProvider blockLogicProvider,
            IEvaluateFunctionProvider evaluateFunctionProvider, IEnumerable<Simulation> simulations, int k)
        {
            _blockLogicProvider = blockLogicProvider;
            _evaluateFunctionProvider = evaluateFunctionProvider;
            _simulations = simulations;
            _k = k;
        }

        /// <summary>
        ///     For each of simulations, executes steps and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="k">k parameter</param>
        /// <param name="step"></param>
        /// <returns>results</returns>
        public async Task<List<Simulation>> Execute(IEnumerable<Simulation> simulations, int k, int step)
        {
            if (simulations == null || !simulations.Any() || k <= 0 || step <= 0)
            {
                throw new AlgorithmLogicException();
            }

            if (_computationsTerminated)
            {
                throw new SimulationTerminatedException();
            }

            return await Task.Run(() =>
            {
                foreach (var simulation in simulations)
                {
                    simulation.LastBlock = new int[simulation.Content.GetLength(0), simulation.Content.GetLength(1)];
                }
                var ret = new List<Simulation>();
                try
                {
                    for (var i = 0; i < step; i++)
                    {
                        var dict = new ConcurrentDictionary<Simulation, int>();
                        Parallel.ForEach(simulations, (simulation) =>
                        {
                            simulation.AvailableBlocks.RemoveAll(b => b.Quantity <= 0);
                            CheckAndCorrectSimulationHeight(simulation);
                            Parallel.ForEach(simulation.AvailableBlocks, (block) =>
                            {
                                Parallel.ForEach(_blockLogicProvider.RotateBlock(block),
                                    (b) =>
                                    {
                                        Parallel.ForEach(
                                            _blockLogicProvider.FindBestPlacesForBlock(
                                                simulation.Content, b.Content),
                                            (xy) =>
                                            {
                                                var sim = AddBlockToSimulation(b, simulation,
                                                    xy.Item1, xy.Item2);
                                                var score =
                                                    _evaluateFunctionProvider.Evaluate(
                                                        sim.Content);
                                                sim.Score = score;
                                                sim.Height = GetSimulationHeight(sim.Content);
                                                sim.Density =
                                                    CountSimulationDensity(sim.Content,
                                                        sim.Height).ToString("0.00");
                                                dict.TryAdd(sim, score);
                                            });
                                    });
                            });
                        });

                        var dictResult = dict.ToDictionary(entry => entry.Key,
                            entry => entry.Value)
                            .Distinct(new SimulationEqualityComparer())
                            .ToDictionary(x => x.Key, x => x.Value);
                        var bestScores =
                            dictResult.Values.OrderByDescending(v => v)
                                .Take(k)
                                .Distinct()
                                .ToList();
                        ret =
                            dictResult.Where(d => bestScores.Contains(d.Value))
                                .Select(p => p.Key)
                                .Take(k)
                                .ToList();
                        simulations = ret;

                        if (simulations.FirstOrDefault()?.AvailableBlocks.Count == 0)
                        {
                            _computationsTerminated = true;
                            break;
                        }
                    }
                }
                catch (OverflowException)
                {
                    throw new AlgorithmLogicException("Not enough space to complete the computations.");
                }

                foreach (var sim in ret)
                {
                    SyncCanvasWithContent(sim);
                }

                return ret;
            });
        }

        private double CountSimulationDensity(int[,] simContent, int simHeight)
        {
            return (double)simContent.Cast<int>().Count(c => c > 0) / (simHeight * simContent.GetLength(0));
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
                Content = (int[,])simulation.Content.Clone(),
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
            var lastBlockId = sim.Content.Cast<int>().Max();

            for (var i = 0; i < block.Height; i++)
            {
                for (var j = 0; j < block.Width; j++)
                {
                    if (!block.Content[i, j])
                    {
                        continue;
                    }

                    if (sim.Content[x + i, y + j] > 0)
                    {
                        throw new AlgorithmLogicException("This place in simulation is already filled");
                    }

                    sim.Content[x + i, y + j] = lastBlockId + 1;
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
                            FillColor = Constants.FillBrushes[simulation.LastBlock[i, j] % Constants.FillBrushes.Count],
                            StrokeColor = Constants.NewBlockStrokeColor,
                            StrokeThickness = Constants.NewBlockStrokeThickness
                        });
                    }
                    else if (simulation.Content[i, j] > 0)
                    {
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth)
                        {
                            FillColor = Constants.FillBrushes[simulation.Content[i, j] % Constants.FillBrushes.Count]
                        });
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
                    if (simulation.Content[i, j] == 0)
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
                    new int[simulation.Content.GetLength(0),
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

        private int GetSimulationHeight(int[,] content)
        {
            var height = content.GetLength(1);
            var width = content.GetLength(0);

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    if (content[i, j] > 0)
                    {
                        return height - j;
                    }
                }
            }

            return 0;
        }
    }
}