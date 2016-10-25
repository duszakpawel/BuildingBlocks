using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        private readonly int _canvasWidth = 600;
        // it may change
        private int _canvasHeight = 800;
        private readonly int _blockWidth;

        private readonly Dispatcher dispatcherThread;


        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            var _step = step;
            if (boardWidth > 50)
            {
                _canvasWidth = boardWidth * 10;
            }
            dispatcherThread = Dispatcher.CurrentDispatcher;
            _blockWidth = _canvasWidth / boardWidth;

            Simulations = new ObservableCollection<Simulation>();
            
            for (var i = 0; i < k; ++i)
            {
                var blocksCopy = new List<Block>();
                foreach (var element in blocks)
                {
                    var  b = new Block
                    {
                        Width = element.Width,
                        Height = element.Height,
                        Content = element.Content,
                        Quantity = element.Quantity,
                        IsQuantityEnabled = element.IsQuantityEnabled,
                    };
                    foreach (var el in element.CanvasChildren)
                    {
                        b.CanvasChildren.Add(new RectItem
                        {
                            Height = el.Height,
                            Width = el.Width,
                            Y = el.Y,
                            X=el.X,
                            FillColor=el.FillColor,
                            StrokeColor=el.StrokeColor
                        });
                    }
                    blocksCopy.Add(b);
                }
                Simulations.Add(new Simulation
                {
                    CanvasChildren = new ObservableCollection<RectItem>(),
                    AvailableBlocks = blocksCopy
                });

            }
        }

        public void Start()
        {
            dispatcherTimer.Start();
        }

        public void Stop()
        {
           dispatcherTimer.Stop();
        }
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public void Pause()
        {
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Next();
        }

        public void Next()
        {
            dispatcherThread.Invoke(() =>
            {
                foreach (var simulation in Simulations)
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
                        element.Y = _canvasHeight- simulation.CurrentHeight - (simulation.AvailableBlocks[r].Height+1.5)*Block.SingleTileWidth + element.Y;
                        simulation.CanvasChildren.Add(element);
                    }
                    simulation.CurrentHeight += simulation.AvailableBlocks[r].Height * Block.SingleTileWidth;
                    simulation.AvailableBlocks.RemoveAt(r);
                }

                
            });
        }
    }
}