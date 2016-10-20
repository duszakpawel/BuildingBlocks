using System.IO;
using BuildingBlocks.BusinessLogic;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public int BoardWidth { get; set; }

        public int K { get; set; }

        public int Step { get; set; }

        public void LoadFile(string name)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return;
            var blocks = new BlocksParser().LoadData(new StreamReader(openFileDialog.FileName));
            BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);
            BoardWidth = blocks.WellWidth;
        }

        public void Start(string name)
        {
           
        }

        public void Stop(string name)
        {

        }

        public void Pause(string name)
        {

        }

        public void Next(string name)
        {

        }
    }
}