using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using BuildingBlocks.BusinessLogic;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        private readonly int _canvasWidth;

        // TODO: it may change
        private int _canvasHeight = 800;

        private readonly int _blockWidth;

        private readonly int _k;

        private int _step;

        private readonly Dispatcher _dispatcherThread;

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            _step = step;
            _k = k;
            _canvasWidth = 600;
            if (boardWidth > 50)
            {
                _canvasWidth = boardWidth * 10;
            }
            _dispatcherThread = Dispatcher.CurrentDispatcher;
            _blockWidth = _canvasWidth / boardWidth;
            Simulations = new ObservableCollection<Simulation>();
            for (var i = 0; i < k; ++i)
            {
                var blocksCopy = new List<Block>();
                foreach (var element in blocks)
                {
                    var b = new Block
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
                            X = el.X,
                            FillColor = el.FillColor,
                            StrokeColor = el.StrokeColor
                        });
                    }
                    blocksCopy.Add(b);
                }
                Simulations.Add(new Simulation
                {
                    CanvasChildren = new ObservableCollection<RectItem>(),
                    AvailableBlocks = blocksCopy,
                    CurrentHeight = 0
                });
            }
        }

        public void Start(int step)
        {
            _step = step;
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
        }

        public void Pause()
        {
            _dispatcherTimer.Stop();
        }

        public void Next(int step)
        {
            _step = step;
            _dispatcherThread.Invoke(() => { Simulations = Algorithm.Execute(Simulations, _canvasHeight); });
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _dispatcherThread.Invoke(() => { Simulations = Algorithm.Execute(Simulations, _canvasHeight); });
        }
    }
}