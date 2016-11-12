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
        public int WellWidth { get; set; } = 100;

        private readonly DispatcherTimer dispatcherTimer;
        private int step;
        private int k;


        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            this.step = step;
            this.k = k;
            WellWidth = boardWidth * Constants.SingleTileWidth;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimerOnTick;

            Simulations = new ObservableCollection<Simulation>();
            for (int i = 0; i < k; i++)
            {
                Simulations.Add(new Simulation()
                {
                    AvailableBlocks = new List<Block>(blocks),
                    CanvasChildren = new ObservableCollection<RectItem>(),
                    CurrentHeight = Constants.SimulationHeight,
                    Content = new bool[boardWidth, Constants.SimulationHeight / Constants.SingleTileWidth]
                });
            }
        }

        public void Start(int step)
        {
            this.step = step;
            dispatcherTimer.Start();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
        }

        public void Pause()
        {
            dispatcherTimer.Stop();
        }

        public void Next(int step)
        {
            this.step = step;
            ExecuteAlgoithmSteps();
        }

        private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            ExecuteAlgoithmSteps();
        }

        private void ExecuteAlgoithmSteps()
        {
            for (int i = 0; i < step; i++)
            {
                Simulations = Algorithm.Execute(Simulations, Constants.SimulationHeight, k);
            }
        }


    }
}