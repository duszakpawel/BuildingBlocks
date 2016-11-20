using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BuildingBlocks.Presentation.Common
{
    /// <summary>
    ///     Custom dialog manager class
    /// </summary>
    internal class CustomDialogManager : ICustomDialogManager
    {
        /// <summary>
        ///     Displays message box on the screen
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public async Task DisplayMessageBox(string title, string message)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            await
                metroWindow.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative,
                    metroWindow.MetroDialogOptions);
        }
    }
}