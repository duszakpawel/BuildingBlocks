using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        private readonly List<Block> _blocks;

        private readonly int _boardWidth;

        private readonly int _k;

        private int _step;

        private readonly int _canvasWidth = 500;

        private readonly int _blockWidth;

        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            _blocks = blocks;
            _boardWidth = boardWidth;
            _k = k;
            _step = step;
            if (_boardWidth > 50)
            {
                _canvasWidth = _boardWidth * 10;
            }
            _blockWidth = _canvasWidth / _boardWidth;
            Simulations = new ObservableCollection<Simulation>();
            for (var i = 0; i < _k; ++i)
            {
                Simulations.Add(new Simulation
                {
                    CanvasChildren = new ObservableCollection<Rectangle>()
                });
            }
        }

        public void Start()
        {
            //throw new System.NotImplementedException();
        }

        public void Stop()
        {
            //throw new System.NotImplementedException();
        }

        public void Pause()
        {
            //throw new System.NotImplementedException();
        }

        public void Next()
        {
            //throw new System.NotImplementedException();
        }
    }
}