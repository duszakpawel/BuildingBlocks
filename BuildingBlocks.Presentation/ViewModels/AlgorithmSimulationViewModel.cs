using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Shapes;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        private readonly int _canvasWidth = 600;
        private readonly int _canvasHeight = 600;
        private readonly int _blockWidth;

        private readonly Thread _computationTread;


        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            var _step = step;
            if (boardWidth > 50)
            {
                _canvasWidth = boardWidth * 10;
            }

            _blockWidth = _canvasWidth / boardWidth;

            Simulations = new ObservableCollection<Simulation>();

            for (var i = 0; i < k; ++i)
            {
                Simulations.Add(new Simulation
                {
                    CanvasChildren = new ObservableCollection<Rectangle>(),
                    AvailableBlocks = blocks
                });

                _computationTread = new Thread(Computations);
            }

        }

        private void Computations()
        {
            foreach (var simulation in Simulations)
            {
                var rnd = new Random();
                var count = simulation.AvailableBlocks.Count;

                if (count == 0)
                {
                    continue;
                }

                var r = rnd.Next(count);

                foreach (var element in simulation.AvailableBlocks[r].CanvasChildren)
                {
                    simulation.CanvasChildren.Add(element);
                }
            }
        }

        public void Start()
        {
            try
            {
                if (_computationTread.ThreadState == ThreadState.StopRequested || _computationTread.ThreadState == ThreadState.Stopped)
                {
                    _computationTread.Start();
                }
                else
                {
                    _computationTread.Start();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Stop()
        {
            try
            {
                if (_computationTread.ThreadState == ThreadState.Running)
                {
                    _computationTread.Abort();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Pause()
        {
            try
            {
                if (_computationTread.IsAlive && _computationTread.ThreadState == ThreadState.Running)
                {
                    _computationTread.Abort();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Next()
        {
            // ignored
        }
    }
}