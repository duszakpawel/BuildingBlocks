using System.Runtime.Serialization;

namespace BuildingBlocks.Models
{
    [DataContract]
    public class BlockSerializer
    {
        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public bool[] Content { get; set; }

        [DataMember]
        public RectItemSerializer[] CanvasChildren { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public bool IsQuantityEnabled { get; set; }
    }
}
