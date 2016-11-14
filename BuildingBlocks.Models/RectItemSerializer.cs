using System.Runtime.Serialization;

namespace BuildingBlocks.Models
{
    [DataContract]
    public class RectItemSerializer
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }
    }
}
