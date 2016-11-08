using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BuildingBlocks.Models
{
    public class Simulation
    {
        public ObservableCollection<RectItem> CanvasChildren { get; set; } = new ObservableCollection<RectItem>();

        public List<Block> UsedBlocks { get; set; } = new List<Block>();

        public List<Block> AvailableBlocks { get; set; } = new List<Block>();

        public double CurrentHeight { get; set; }
    }
}
