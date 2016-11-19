using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BuildingBlocks.Models;

namespace BuildingBlocks.BusinessLogic.Algorithm
{
    public class AlgorithmSolver
    {
        private IEnumerable<Simulation> _simulations;
        private int _k;

        public AlgorithmSolver(IEnumerable<Simulation> simulations, int k)
        {
            _simulations = simulations;
            _k = k;
        }
        public ObservableCollection<Simulation> Execute()
        {
            var dict = new Dictionary<Simulation, int>();
            foreach (var simulation in _simulations)
            {
                CheckAndCorrectSimulationHeight(simulation);
                foreach (var block in simulation.AvailableBlocks)
                {
                    foreach (var b in BlockLogic.RotateBlock(block))
                    {
                        var xy = BlockLogic.FindBestPlaceForBlock(simulation.Content, b.Content);
                        var sim = AddBlockToSimulation(b, simulation, xy.Item1, xy.Item2);
                        var score = EvaluateFunction.Evaluate(sim.Content);
                        dict.Add(sim, score);
                    }
                }
            }
            dict = dict.Distinct(new SimulationEqualityComparer()).ToDictionary(x => x.Key, x => x.Value);

            var bestScores = dict.Values.OrderByDescending(v => v).Take(_k).Distinct().ToList();
            var ret = new ObservableCollection<Simulation>(dict.Where(d => bestScores.Contains(d.Value)).Select(p => p.Key).Take(_k));
            foreach (var sim in ret)
            {
                SyncCanvasWithContent(sim);
            }
            return ret;
        }

        // x and y are coordinates of top left corner of block
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

        private void SyncCanvasWithContent(Simulation simulation)
        {
            var children = new ObservableCollection<RectItem>();
            for (var i = 0; i < simulation.Content.GetLength(0); i++)
            {
                for (var j = 0; j < simulation.Content.GetLength(1); j++)
                {
                    if (simulation.LastBlock[i, j])
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth)
                        {
                            FillColor = Constants.BlockFillColor
                        });
                    else if (simulation.Content[i, j])
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth));
                }
            }
            simulation.CanvasChildren = children;
        }

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
                for (var ii = 0; ii < simulation.Content.GetLength(0); ii++)
                {
                    for (var jj = 0; jj < simulation.Content.GetLength(1); jj++)
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
