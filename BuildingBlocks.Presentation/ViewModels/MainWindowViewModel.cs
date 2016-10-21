using System.ComponentModel;
using System.IO;
using BuildingBlocks.BusinessLogic;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen, IDataErrorInfo
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public string BoardWidth { get { return _boardWidth; } set { _boardWidth = value; EnableStart(); EnableNext(); } }

        public string K { get { return _k; } set { _k = value; EnableStart(); EnableNext(); } }

        public string Step { get { return _step; } set { _step = value; EnableNext(); } }

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
        }

        public void LoadFile(string name)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            var blocks = new BlocksParser().LoadData(new StreamReader(openFileDialog.FileName));
            BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);
            BoardWidth = blocks.WellWidth.ToString();
        }

        public void Start(string name)
        {
            IsStartEnabled = false;
            IsStopEnabled = true;
            IsPauseEnabled = true;
            IsKEnabled = false;
            IsLoadFileEnabled = false;
            IsExpanded = false;
            IsNextEnabled = false;
        }

        public void Stop(string name)
        {
            EnableStart();
            IsStopEnabled = false;
            IsPauseEnabled = false;
            IsKEnabled = true;
            IsLoadFileEnabled = true;
            IsExpanded = true;
            EnableNext();
        }

        public void Pause(string name)
        {
            EnableStart();
            IsPauseEnabled = false;
            EnableNext();
        }

        public void Next(string name)
        {

        }

        public string this[string columnName]
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (columnName == "K")
                {
                    if (!IsNaturalNumber(K)) return "Value must natural number";
                }
                else if (columnName == "Step")
                {
                    if (!IsNaturalNumber(Step)) return "Value must natural number";
                }
                else if (columnName == "BoardWidth")
                {
                    if (!IsNaturalNumber(BoardWidth)) return "Value must natural number";
                }
                return null;
            }
        }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string Error { get; }

        private void EnableStart()
        {
            if (IsNaturalNumber(K) && IsNaturalNumber(BoardWidth))
            {
                IsStartEnabled = true;
                IsStepEnabled = true;
            }
            else
            {
                IsStartEnabled = false;
                IsStepEnabled = false;
            }
        }

        private void EnableNext()
        {
            if (IsNaturalNumber(K) && IsNaturalNumber(BoardWidth) && IsNaturalNumber(Step) && IsStartEnabled)
            {
                IsNextEnabled = true;
            }
            else
            {
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