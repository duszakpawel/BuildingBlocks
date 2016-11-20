using System.Windows.Controls;
using BuildingBlocks.Presentation.ViewModels;

namespace BuildingBlocks.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}