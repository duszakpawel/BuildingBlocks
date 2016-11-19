using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using BuildingBlocks.BusinessLogic;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;
using Microsoft.Win32;
using BuildingBlocks.Models;
using BuildingBlocks.Models.Models;

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    /// Main window view model class.
    /// </summary>
    public class MainWindowViewModel : Screen
    {
        /// <summary>
        /// Reference to blocks browser view model
        /// </summary>
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        /// <summary>
        /// Reference to algorithms simulation view model
        /// </summary>
        public AlgorithmSimulationViewModel AlgorithmSimulationViewViewModel { get; set; }

        /// <summary>
        /// Board width
        /// </summary>
        public int BoardWidth { get; set; }

        /// <summary>
        /// K parameter
        /// </summary>
        public int K { get; set; } = 1;

        /// <summary>
        /// Step value
        /// </summary>
        public int Step { get; set; } = 1;

        /// <summary>
        /// Returns information whether Start button is enabled or disabled
        /// </summary>
        public bool CanStart => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        /// <summary>
        /// Returns information whether Stop button is enabled or disabled
        /// </summary>
        public bool CanStop { get; set; }

        /// <summary>
        /// Returns information whether Pause button is enabled or disabled
        /// </summary>
        public bool CanPause { get; set; }

        /// <summary>
        /// Returns information whether Next button is enabled or disabled
        /// </summary>
        public bool CanNext => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        /// <summary>
        /// Returns information whether LoadFile button is enabled or disabled
        /// </summary>
        public bool CanLoadFile { get; set; } = true;

        /// <summary>
        /// Returns information whether Save button is enabled or disabled
        /// </summary>
        public bool CanSave => AlgorithmSimulationViewViewModel != null;

        /// <summary>
        /// Returns information whether Load button is enabled or disabled
        /// </summary>
        public bool CanLoad => AlgorithmSimulationViewViewModel == null;

        /// <summary>
        /// Returns information whether computations are in progress or not
        /// </summary>
        public bool IsProcessing { get; set; }

        /// <summary>
        /// Returns information whether top menu is collapsed or not
        /// </summary>
        public bool IsExpanded { get; set; } = true;

        /// <summary>
        /// Returns information whether K input is enabled or disabled
        /// </summary>
        public bool IsKEnabled { get; set; } = true;

        /// <summary>
        /// Returns information whether Step control is enabled or disabled
        /// </summary>
        public bool IsStepEnabled { get; set; } = true;

        /// <summary>
        /// Load file button onclick handler
        /// </summary>
        public async void LoadFile()
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

        /// <summary>
        /// Start button onclick handler
        /// </summary>
        public void Start()
        {
            IsProcessing = true;
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = true;
            IsKEnabled = false;
            IsStepEnabled = false;
            if(BlocksBrowserViewViewModel == null)
            {
                BlocksBrowserViewViewModel = new BlocksBrowserViewModel(new List<Block>());
            }
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Start(Step);
        }

        /// <summary>
        /// Stop button onclick handler
        /// </summary>
        public void Stop()
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

        /// <summary>
        /// Pause button onclick handler
        /// </summary>
        public void Pause()
        {
            IsProcessing = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            AlgorithmSimulationViewViewModel.Pause();
        }

        /// <summary>
        /// Next button onclick handler
        /// </summary>
        public void Next()
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

        /// <summary>
        /// Save button onclick handler
        /// </summary>
        public void Save()
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = "xml files (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            // TODO: DI
            var computationsSerializer = new ComputationsSerializer();
            computationsSerializer.Serialize(openFileDialog.FileName, BoardWidth, K, AlgorithmSimulationViewViewModel.Simulations);
        }

        /// <summary>
        /// Load file onclick handler
        /// </summary>
        public void Load()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "xml files (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }
            // TODO: DI
            var computationsSerializer = new ComputationsSerializer();
            var result = computationsSerializer.Deserialize(openFileDialog.FileName);

            var boardWidth = result.Item1;
            var k = result.Item2;
            var Simulations = result.Item3;
            BoardWidth = boardWidth;
            K = k;

            AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(new List<Block>(), BoardWidth, K, Step);

            AlgorithmSimulationViewViewModel.Simulations = new ObservableCollection<Simulation>(Simulations);
            Start();
            Pause();
        }
    }
}