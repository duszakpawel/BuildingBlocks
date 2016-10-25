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

        public int BoardWidth { get; set; }

        public int K { get; set; } = 1;

        public int Step { get; set; } = 1;

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

        public bool IsStartEnabled { get; set; }

        public bool IsStopEnabled { get; set; }

        public bool IsPauseEnabled { get; set; }

        public bool IsNextEnabled { get; set; }

        public bool IsExpanded { get; set; } = true;

        private bool _isStepEnabled = true;

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

            IsStartEnabled = true;
            IsPauseEnabled = false;
            IsStopEnabled = false;
            IsStepEnabled = true;
            IsKEnabled = true;
            IsNextEnabled = true;
        }

        public void Start(string name)
        {
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);

            IsStartEnabled = false;
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

            IsStartEnabled = true;
            IsStopEnabled = false;
            IsPauseEnabled = false;
            IsKEnabled = true;
            IsLoadFileEnabled = true;
            IsExpanded = true;
            IsStepEnabled = true;
            IsNextEnabled = true;

            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        public void Pause(string name)
        {

            IsStartEnabled = true;
            IsPauseEnabled = false;
            IsStepEnabled = true;
            IsNextEnabled = true;
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


    }
}