using System.IO;
using BuildingBlocks.BusinessLogic;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public AlgorithmSimulationViewModel AlgorithmSimulationViewViewModel { get; set; }

        public int BoardWidth
        {
            get
            {
                return _boardWidth;
            }
            set
            {
                _boardWidth = value; EnableStart();
            }
        }

        public int K
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value; EnableStart();
            }
        }

        public int Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value; EnableStart();
            }
        }
        public bool IsStepEnabled
        {
            get
            {
                return _isStepEnabled;

            }
            set
            {
                _isStepEnabled = value;
                NotifyOfPropertyChange(nameof(IsStepEnabled));
            }
        }

        public bool IsLoadFileEnabled { get; set; } = true;

        public bool IsKEnabled { get; set; } = true;

        public bool IsStartEnabled => K > 0 && BoardWidth > 0;

        public bool IsStopEnabled { get; set; }

        public bool IsPauseEnabled { get; set; }

        public bool IsNextEnabled { get; set; }

        public bool IsExpanded { get; set; } = true;

        private int _boardWidth;
        private int _k;
        private int _step = 1;
        private bool _isStepEnabled;

        public async void LoadFile(string name)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            var blocks = await new BlocksParser().LoadData(new StreamReader(openFileDialog.FileName));
            BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);
            BoardWidth = blocks.WellWidth;
        }

        public void Start(string name)
        {
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);

            IsStopEnabled = true;
            IsPauseEnabled = true;
            IsKEnabled = false;
            IsLoadFileEnabled = false;
            IsExpanded = false;
            IsNextEnabled = false;
            IsStepEnabled = false;

            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }

            AlgorithmSimulationViewViewModel.Start();
        }

        public void Stop(string name)
        {
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.All);
            EnableStart();

            IsStopEnabled = false;
            IsPauseEnabled = false;
            IsKEnabled = true;
            IsLoadFileEnabled = true;
            IsExpanded = true;
            IsStepEnabled = true;

            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        public void Pause(string name)
        {
            EnableStart();

            IsPauseEnabled = false;
            IsStepEnabled = true;

            AlgorithmSimulationViewViewModel.Pause();
        }

        public void Next(string name)
        {
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }

            AlgorithmSimulationViewViewModel.Next();
            IsStopEnabled = true;
        }

        private void EnableStart()
        {
            IsStepEnabled = true;
            IsNextEnabled = true;
        }
    }
}