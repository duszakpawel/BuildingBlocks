using System;
using System.Collections.ObjectModel;
using System.Threading;
using BuildingBlocks.Models;
using System.Windows.Media;

namespace BuildingBlocks.BusinessLogic
{
    public static class Algorithm
    {
        // TODO: Tutaj zaimplementuj algorytm, mozna przekazac wiecej parametrow z algorithmviewmodel, caly kod ponizej to syf do wywalenia
        public static ObservableCollection<Simulation> Execute(ObservableCollection<Simulation> simulations, int canvasHeight)
        {
            foreach (var simulation in simulations)
            {
                var rnd = new Random();
                Thread.Sleep(50);
                var count = simulation.AvailableBlocks.Count;
                var r = rnd.Next(count);

                if (count == 0)
                {
                    continue;
                }
                var tmp = new ObservableCollection<RectItem>();
                foreach (var el in simulation.CanvasChildren)
                {
                    el.FillColor = Brushes.Gray;
                    tmp.Add(el);
                }
                foreach (var elem in tmp)
                {
                    simulation.CanvasChildren.Remove(elem);
                    simulation.CanvasChildren.Add(elem);

                }
                foreach (var element in simulation.AvailableBlocks[r].CanvasChildren)
                {
                    element.Y = canvasHeight - simulation.CurrentHeight - (simulation.AvailableBlocks[r].Height + 1.5) * Block.SingleTileWidth + element.Y;
                    simulation.CanvasChildren.Add(element);
                }
                simulation.CurrentHeight += simulation.AvailableBlocks[r].Height * Block.SingleTileWidth;
                simulation.AvailableBlocks.RemoveAt(r);
            }
            return simulations;
        }
    }
}
