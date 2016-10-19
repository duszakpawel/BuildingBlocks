using System.Collections.Generic;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class BlocksBrowserViewModel : Screen
    {
         public List<Block> Blocks { get; set; }
    }
}