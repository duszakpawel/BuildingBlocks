using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using PropertyChanged;

namespace BuildingBlocks.Models
{
    public class Simulation
    {
        public ObservableCollection<Rectangle> CanvasChildren { get; set; } = new ObservableCollection<Rectangle>(new List<Rectangle>());
        public List<Block> UsedBlocks { get; set; }=  new List<Block>();
        public List<Block> AvailableBlocks { get; set; }= new List<Block>();
        public Simulation Result { get; set; }
    }
}
