using System.Runtime.Serialization;

namespace BuildingBlocks.Models
{
    [DataContract]
    public class SimulationSerializer
    {
        [DataMember]
        public RectItemSerializer[] CanvasChildren { get; set; } 

        [DataMember]
        public BlockSerializer[] AvailableBlocks { get; set; }

        [DataMember]
        public int WellHeight { get; set; }

        [DataMember]
        public bool[] Content { get; set; }

        [DataMember]
        public bool[] LastBlock { get; set; }
    }
}
