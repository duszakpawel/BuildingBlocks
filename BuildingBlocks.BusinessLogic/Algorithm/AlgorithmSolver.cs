using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Models.Constants;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    /// Algorithm solver
    /// </summary>
    public class AlgorithmSolver
    {
        private IEnumerable<Simulation> simulations;
        private int _k;
        private BlockLogicProvider _blockLogicProvider;
        private EvaluateFunctionProvider _evaluateFunctionProvider;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="_k">k parameter</param>
        public AlgorithmSolver(IEnumerable<Simulation> simulations, int _k)
        {
            this.simulations = simulations;
            this._k = _k;
            _blockLogicProvider = new BlockLogicProvider();
            _evaluateFunctionProvider = new EvaluateFunctionProvider();
        }

        /// <summary>
        /// For each of simulations, executes one step and takes the best k simulations at the end.
        /// </summary>
        /// <param name="simulations">simulations collection</param>
        /// <param name="_k">k parameter</param>
        /// <returns></returns>
        public List<Simulation> Execute(IEnumerable<Simulation> simulations, int _k)
        {
            var simulationScoreDictionary = new Dictionary<Simulation, int>();

            foreach (var simulation in simulations)
            {
                CheckAndCorrectSimulationHeight(simulation);

                foreach (var block in simulation.AvailableBlocks)
                {
                    foreach (var rotatedBlock in _blockLogicProvider.RotateBlock(block))
                    {
                        var xy = _blockLogicProvider.FindBestPlaceForBlock(simulation.Content, rotatedBlock.Content);
                        var sim = AddBlockToSimulation(rotatedBlock, simulation, xy.Item1, xy.Item2);
                        var score = _evaluateFunctionProvider.Evaluate(sim.Content);
                        simulationScoreDictionary.Add(sim, score);
                    }
                }
            }
            simulationScoreDictionary = simulationScoreDictionary.Distinct(new SimulationEqualityComparer()).ToDictionary(x => x.Key, x => x.Value);

            var bestScores = simulationScoreDictionary.Values.OrderByDescending(v => v).Take(_k).Distinct().ToList();
            var result = new ObservableCollection<Simulation>(simulationScoreDictionary.Where(d => bestScores.Contains(d.Value)).Select(p => p.Key).Take(_k));

            foreach (var sim in result)
            {
                SyncCanvasWithContent(sim);
            }

            return result.ToList();
        }

        /// <summary>
        /// Adds new block to simulation. x and y are coordinates of top left corner of block
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
                LastBlock = new bool[simulation.Content.GetLength(0), simulation.Content.GetLength(1)]
            };

            var bl = sim.AvailableBlocks.Single(b => b.Id == block.Id);
            bl.Quantity--;
            if (bl.Quantity == 0)
                sim.AvailableBlocks.Remove(bl);
            
            for (var i = 0; i < block.Height; i++)
            {
                for (var j = 0; j < block.Width; j++)
                {
                    if (!block.Content[i, j]) continue;
                    if (sim.Content[x + i, y + j])
                        throw new ArgumentException("This place in simulation is already filled");
                    sim.Content[x + i, y + j] = true;
                    sim.LastBlock[x + i, y + j] = true;
                }
            }
            return sim;
        }

        /// <summary>
        /// Updates colors of blocks and produces new rectangle items collection to be displayed
        /// </summary>
        /// <param name="simulation">simulation object</param>
        private void SyncCanvasWithContent(Simulation simulation)
        {
            var children = new ObservableCollection<RectItem>();

            for (var i = 0; i < simulation.Content.GetLength(0); i++)
            {
                for (var j = 0; j < simulation.Content.GetLength(1); j++)
                {
                    if (simulation.LastBlock[i, j])
                    {
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth)
                        {
                            FillColor = Constants.BlockFillColor
                        });
                    }
                    else if (simulation.Content[i, j])
                    {
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth));
                    }
                }
            }

            simulation.CanvasChildren = children;
        }

        /// <summary>
        /// corrects simulation height
        /// </summary>
        /// <param name="simulation">simulation object</param>
        private void CheckAndCorrectSimulationHeight(Simulation simulation)
        {
            for (var j = 0; j < Constants.CompulsoryFreeSpaceInWellHeight; j++)
            {
                var free = true;
                for (var i = 0; i < simulation.Content.GetLength(0); i++)
                {
                    if (!simulation.Content[i, j]) continue;
                    free = false;
                    break;
                }
                if (free) continue;

                // make well bigger: 
                simulation.WellHeight += Constants.CompulsoryFreeSpaceInWellHeight * Constants.SingleTileWidth;
                var newContent = new bool[simulation.Content.GetLength(0), simulation.Content.GetLength(1) + Constants.CompulsoryFreeSpaceInWellHeight];
                for (var i = 0; i < simulation.Content.GetLength(0); i++)
                {
                    for (var k = 0; k < simulation.Content.GetLength(1); k++)
                    {
                        newContent[i, k + Constants.CompulsoryFreeSpaceInWellHeight] = simulation.Content[i, k];
                    }
                }
                simulation.Content = newContent;
                return;
            }
        }
    }
}
