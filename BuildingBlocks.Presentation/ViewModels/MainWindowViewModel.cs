using System.IO;
using BuildingBlocks.BusinessLogic;
using Caliburn.Micro;
using Microsoft.Win32;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public void LoadFile(string name)
        {
            string fileContent;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var blocks = new BlocksParser().LoadBlocks(openFileDialog.FileName);
            }
        }
    }
}