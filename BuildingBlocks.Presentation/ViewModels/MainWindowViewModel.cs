using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;
using Microsoft.Win32;
using BuildingBlocks.BusinessLogic.Exceptions;
using System.Runtime.Serialization;
using BuildingBlocks.Models.Constants;
#pragma warning disable 4014

namespace BuildingBlocks.Presentation.ViewModels
{
    /// <summary>
    ///     Main window view model class.
    /// </summary>
    public class MainWindowViewModel : Screen
    {
        private readonly IComputationsSerializer _computationsSerializer;
        /// <summary>
        ///     Reference to blocks browser view model
        /// </summary>
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; private set; }

        /// <summary>
        ///     Reference to algorithms simulation view model
        /// </summary>
        public AlgorithmSimulationViewModel AlgorithmSimulationViewViewModel { get; private set; }

        /// <summary>
        ///     Board width
        /// </summary>
        public int BoardWidth { get; set; }

        /// <summary>
        /// Is changing quantity of every block enabled flag
        /// </summary>
        public bool IsChangingQuantityOfEveryBlockEnabled { get; set; }

        private int _quantityOfEveryBlock = 1;

        /// <summary>
        /// Quantity of every block
        /// </summary>
        public int QuantityOfEveryBlock
        {
            get { return _quantityOfEveryBlock; }
            set
            {
                _quantityOfEveryBlock = value;
                BlocksBrowserViewViewModel.SetQuantityOfEveryBlock(_quantityOfEveryBlock);
            }
        }

        /// <summary>
        ///     K parameter
        /// </summary>
        public int K { get; set; } = Constants.KDefaultValue;

        /// <summary>
        ///     Step value
        /// </summary>
        public int Step { get; set; } = Constants.StepDefaultValue;

        /// <summary>
        ///     Returns information whether Start button is enabled or disabled
        /// </summary>
        public bool CanStart => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        /// <summary>
        ///     Returns information whether Stop button is enabled or disabled
        /// </summary>
        public bool CanStop { get; set; }

        /// <summary>
        ///     Returns information whether Pause button is enabled or disabled
        /// </summary>
        public bool CanPause { get; set; }

        /// <summary>
        ///     Returns information whether Next button is enabled or disabled
        /// </summary>
        public bool CanNext => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        /// <summary>
        ///     Returns information whether LoadFile button is enabled or disabled
        /// </summary>
        public bool CanLoadFile { get; set; } = true;

        /// <summary>
        ///     Returns information whether Save button is enabled or disabled
        /// </summary>
        public bool CanSave => AlgorithmSimulationViewViewModel != null;

        /// <summary>
        ///     Returns information whether Load button is enabled or disabled
        /// </summary>
        public bool CanLoad => AlgorithmSimulationViewViewModel == null;

        /// <summary>
        ///     Returns information whether computations are in progress or not
        /// </summary>
        public bool IsProcessing { get; set; }

        /// <summary>
        ///     Returns information whether top menu is collapsed or not
        /// </summary>
        public bool IsExpanded { get; set; } = true;

        /// <summary>
        ///     Returns information whether K input is enabled or disabled
        /// </summary>
        public bool IsKEnabled { get; set; } = true;

        /// <summary>
        ///     Returns information whether Step control is enabled or disabled
        /// </summary>
        public bool IsStepEnabled { get; set; } = true;

        public MainWindowViewModel(IComputationsSerializer computationsSerializer)
        {
            _computationsSerializer = computationsSerializer;
        }

        /// <summary>
        ///     Load file button onclick handler
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

            try
            {
                var blocks = await IoC.Get<IBlocksParser>().LoadData(new StreamReader(openFileDialog.FileName));
                BlocksBrowserViewViewModel = new BlocksBrowserViewModel(IoC.Get<IBlocksPreprocessor>(), blocks.Blocks);
                BoardWidth = blocks.WellWidth;
                IsChangingQuantityOfEveryBlockEnabled = true;
            }
            catch (ParsingException details)
            {
                var dialogManager = IoC.Get<ICustomDialogManager>();
               
                    dialogManager.DisplayMessageBox("Information",
                        $"The file is incorrect. Operation terminated. {details.Message}");
            }
        }

        /// <summary>
        ///     Start button onclick handler
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
            IsChangingQuantityOfEveryBlockEnabled = false;

            if (BlocksBrowserViewViewModel == null)
            {
                BlocksBrowserViewViewModel = new BlocksBrowserViewModel(IoC.Get<IBlocksPreprocessor>(), new List<Block>());
            }
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel =
                    new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Start(Step);
        }

        /// <summary>
        ///     Stop button onclick handler
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
            IsChangingQuantityOfEveryBlockEnabled = true;
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.All);
            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        /// <summary>
        ///     Pause button onclick handler
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
        ///     Next button onclick handler
        /// </summary>
        public async void Next()
        {
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel =
                    new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }

            await AlgorithmSimulationViewViewModel.Next(Step);
        }

        /// <summary>
        ///     Save button onclick handler
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

            try
            {
                _computationsSerializer.Serialize(openFileDialog.FileName, BoardWidth, K,
                    AlgorithmSimulationViewViewModel.Simulations);
            }
            catch (SerializationException)
            {
                var dialogManager = IoC.Get<ICustomDialogManager>();
                dialogManager.DisplayMessageBox("Information", "An error occured. Saving to XML file terminated.");
            }
        }

        /// <summary>
        ///     Load file onclick handler
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

            try
            {
                var result = _computationsSerializer.Deserialize(openFileDialog.FileName);
                var boardWidth = result.Item1;
                var k = result.Item2;
                var simulations = result.Item3;
                BoardWidth = boardWidth;
                K = k;

                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(new List<Block>(), BoardWidth, K,
                    Step) {Simulations = new ObservableCollection<Simulation>(simulations)};

                Start();
                Pause();
            }
            catch (SerializationException)
            {
                var dialogManager = IoC.Get<ICustomDialogManager>();
                dialogManager.DisplayMessageBox("Information", "The file is incorrect. Operation terminated.");
            }
        }
    }
}