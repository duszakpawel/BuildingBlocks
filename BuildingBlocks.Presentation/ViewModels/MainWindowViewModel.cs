using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using BuildingBlocks.BusinessLogic;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public enum DisplayMode
    {
        Selected,
        All
    }

    public class MainWindowViewModel : Screen, IDataErrorInfo
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public AlgorithmSimulationViewModel AlgorithmSimulationViewViewModel { get; set; }

        public string BoardWidth { get { return _boardWidth; } set { _boardWidth = value; EnableStart(); } }

        public string K { get { return _k; } set { _k = value; EnableStart(); } }

        public string Step { get { return _step; } set { _step = value; EnableStart(); } }

        public bool IsLoadFileEnabled { get; set; }

        public bool IsKEnabled { get; set; }

        public bool IsStartEnabled { get; set; }

        public bool IsStopEnabled { get; set; }

        public bool IsPauseEnabled { get; set; }

        public bool IsNextEnabled { get; set; }

        public bool IsStepEnabled { get; set; }

        public bool IsExpanded { get; set; }

        private string _boardWidth;
        private string _k;
        private string _step;

        public MainWindowViewModel()
        {
            IsExpanded = true;
            IsKEnabled = true;
            IsLoadFileEnabled = true;
            IsNextEnabled = false;
            IsPauseEnabled = false;
            IsStopEnabled = false;
            IsStepEnabled = false;
            Step = "1";
        }
        public async void LoadFile(string name)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            var blocks = await new BlocksParser().LoadData(new StreamReader(openFileDialog.FileName));
            BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);
            BoardWidth = blocks.WellWidth.ToString();
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
            if (AlgorithmSimulationViewViewModel == null)
            {
                int tempStep;
                if (!int.TryParse(Step, out tempStep)) { tempStep = 1; }
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks,
                    int.Parse(BoardWidth), int.Parse(K), tempStep);
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
            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        public void Pause(string name)
        {
            EnableStart();
            IsPauseEnabled = false;
            AlgorithmSimulationViewViewModel.Pause();
        }

        public void Next(string name)
        {
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks,
                    int.Parse(BoardWidth), int.Parse(K), int.Parse(Step));
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Next();
            IsStopEnabled = true;
        }

        public string this[string columnName]
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (columnName == "K")
                {
                    if (!IsNaturalNumber(K)) return "Value must be a natural number";
                }
                else if (columnName == "Step")
                {
                    if (!IsNaturalNumber(Step)) return "Value must be a natural number";
                }
                else if (columnName == "BoardWidth")
                {
                    if (!IsNaturalNumber(BoardWidth)) return "Value must be a natural number";
                }
                return null;
            }
        }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string Error { get; }

        private void EnableStart()
        {
            if (IsNaturalNumber(K) && IsNaturalNumber(BoardWidth) && IsNaturalNumber(Step))
            {
                IsStartEnabled = true;
                IsStepEnabled = true;
                IsNextEnabled = true;
            }
            else
            {
                IsStartEnabled = false;
                IsNextEnabled = false;
            }
        }

        private static bool IsNaturalNumber(string param)
        {
            int parsed;
            if (!int.TryParse(param, out parsed))
            {
                return false;
            }
            return parsed >= 1;
        }
    }
}