using System.Threading.Tasks;

namespace BuildingBlocks.Presentation.Common
{
    internal interface ICustomDialogManager
    {
        Task DisplayMessageBox(string title, string message);
    }
}