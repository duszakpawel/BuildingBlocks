using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using BuildingBlocks.BusinessLogic;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public void LoadFile(string name)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var blocks = new BlocksParser().LoadData(
                    new StreamReader(openFileDialog.FileName));
                BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);

            }
        }
    }
}