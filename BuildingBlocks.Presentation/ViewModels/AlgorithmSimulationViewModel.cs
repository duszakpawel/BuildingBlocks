using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        public int WellWidth { get; set; }

        private readonly DispatcherTimer _dispatcherTimer;

        private int _step;

        private readonly int _k;

        private bool _simulationFinished = false;

        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            _step = step;
            _k = k;
            WellWidth = boardWidth * Constants.SingleTileWidth;
            _dispatcherTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            _dispatcherTimer.Tick += DispatcherTimerOnTick;
            Simulations = new ObservableCollection<Simulation>();
            for (var i = 0; i < k; i++)
            {
                var list = new List<Block>();
                foreach (var block in blocks)
                {
                    list.Add(new Block(block));
                }
                Simulations.Add(new Simulation
                {
                    AvailableBlocks = list,
                    CanvasChildren = new ObservableCollection<RectItem>(),
                    WellHeight = Constants.SimulationStartHeight,
                    Content = new bool[boardWidth, Constants.SimulationStartHeight / Constants.SingleTileWidth]
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
            if (_simulationFinished)
            {
                return;
            }

            _step = step;
            ExecuteAlgoithmSteps();
        }

        private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            if (_simulationFinished)
            {
                return;
            }

            ExecuteAlgoithmSteps();
        }

        private void ExecuteAlgoithmSteps()
        {
            for (var i = 0; i < _step; i++)
            {
                var result = Algorithm.Execute(Simulations, _k);

                if(result.Count > 0)
                {
                    Simulations = Algorithm.Execute(Simulations, _k);
                }
                else
                {
                    Pause();
                    _simulationFinished = true;
                }
            }
        }
    }
}