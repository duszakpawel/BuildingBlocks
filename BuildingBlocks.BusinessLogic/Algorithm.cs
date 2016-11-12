using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using BuildingBlocks.Models;
using System.Windows.Media;

namespace BuildingBlocks.BusinessLogic
{
    public static class Algorithm
    {
        private static int yy = 25;
        public static void Execute(ObservableCollection<Simulation> simulations, int canvasHeight)
        {
            foreach (var simulation in simulations)
            {
                int rnd = new Random().Next(0, simulation.AvailableBlocks.Count - 1);
                var block = simulation.AvailableBlocks[rnd];
                AddBlockToSimulation(block, simulation, 0, yy);
                yy -= 10;
            }
        }

        // x and y are coordinates of top left corner of block
        private static void AddBlockToSimulation(Block block, Simulation simulation, int x, int y)
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.Content[i, j])
                    {
                        if (simulation.Content[x + i, y + j])
                            throw new ArgumentException("This place in simulation is already filled");
                        simulation.Content[x + i, y + j] = true;
                    }
                }
            }
            SyncCanvasWithContent(simulation);
        }

        private static void SyncCanvasWithContent(Simulation simulation)
        {
            var children = new ObservableCollection<RectItem>();
            for (int i = 0; i < simulation.Content.GetLength(0); i++)
            {
                for (int j = 0; j < simulation.Content.GetLength(1); j++)
                {
                    if (simulation.Content[i, j])
                        children.Add(new RectItem(i * Constants.SingleTileWidth, j * Constants.SingleTileWidth));
                }
            }
            simulation.CanvasChildren = children;
        }
    }
}
