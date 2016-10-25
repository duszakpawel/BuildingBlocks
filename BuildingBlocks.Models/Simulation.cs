using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using PropertyChanged;

namespace BuildingBlocks.Models
{
    public class Simulation
    {
        public ObservableCollection<RectItem> CanvasChildren { get; set; } = new ObservableCollection<RectItem>();
        public List<Block> UsedBlocks { get; set; }=  new List<Block>();
        public List<Block> AvailableBlocks { get; set; }= new List<Block>();
        public Simulation Result { get; set; }
        public double CurrentHeight { get; set; }

    }
}
