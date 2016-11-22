using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    ///     Algorithm simulation view model
    /// </summary>
    public class AlgorithmSimulationViewModel : Screen
    {
        private readonly IAlgorithmSolver _algorithmSolver;

        private readonly DispatcherTimer _dispatcherTimer;

        private readonly int _k;

        private bool _simulationFinished;

        private int _step;

        /// <summary>
        ///     simulations collection
        /// </summary>
        public ObservableCollection<Simulation> Simulations { get; set; }

        /// <summary>
        ///     well width
        /// </summary>
        public int WellWidth { get; private set; }

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="blocks">blocks collection</param>
        /// <param name="boardWidth">board width</param>
        /// <param name="k">k parameter</param>
        /// <param name="step">step value</param>
        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            _step = step;
            _k = k;
            WellWidth = boardWidth*Constants.SingleTileWidth;

            _dispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
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
                    Content = new int[boardWidth, Constants.SimulationStartHeight/Constants.SingleTileWidth]
                });
            }

            _algorithmSolver = new AlgorithmSolver(IoC.Get<IBlockLogicProvider>(), IoC.Get<IEvaluateFunctionProvider>(),
                Simulations, _k);
        }

        /// <summary>
        ///     Start command
        /// </summary>
        /// <param name="step">step value</param>
        public void Start(int step)
        {
            _step = step;
            _dispatcherTimer.Start();
        }

        /// <summary>
        ///     Stop command
        /// </summary>
        public void Stop()
        {
            _dispatcherTimer.Stop();
        }

        /// <summary>
        ///     Pause command
        /// </summary>
        public void Pause()
        {
            _dispatcherTimer.Stop();
        }

        /// <summary>
        ///     Next computations command
        /// </summary>
        /// <param name="step">step value</param>
        public async Task Next(int step)
        {
            if (_simulationFinished)
            {
                return;
            }

            _step = step;
            await ExecuteAlgorithmSteps();
        }

        private async void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            if (_simulationFinished)
            {
                return;
            }

            await ExecuteAlgorithmSteps();
        }

        private async Task ExecuteAlgorithmSteps()
        {
            var result = await _algorithmSolver.Execute(Simulations, _k, _step);

            if (result.Count > 0)
            {
                Simulations = new ObservableCollection<Simulation>(result);
            }
            else
            {
                Pause();
                _simulationFinished = true;
            }
        }
    }
}