using System.IO;
using System.Linq;
using System.Security.Permissions;
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

        public bool CanStart => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        public bool CanStop { get; set; }

        public bool CanPause { get; set; }

        public bool CanNext => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        public bool CanLoadFile { get; set; } = true;

        public bool IsProcessing { get; set; }

        public bool IsExpanded { get; set; } = true;

        public bool IsKEnabled { get; set; } = true;

        public bool IsStepEnabled { get; set; } = true;

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
            IsProcessing = true;
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = true;
            IsKEnabled = false;
            IsStepEnabled = false;
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Start(Step);
        }

        public void Stop(string name)
        {
            IsProcessing = false;
            CanLoadFile = true;
            IsExpanded = true;
            CanStop = false;
            CanPause = false;
            IsKEnabled = true;
            IsStepEnabled = true;
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.All);
            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        public void Pause(string name)
        {
            IsProcessing = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            AlgorithmSimulationViewViewModel.Pause();
        }

        public void Next(string name)
        {
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Next(Step);    
        }
    }
}