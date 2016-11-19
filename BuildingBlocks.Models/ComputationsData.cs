using System.Runtime.Serialization;

namespace BuildingBlocks.Models
{
    [DataContract]
    public class ComputationsData
    {
        [DataMember]
        public int K { get; set; }

        [DataMember]
        public int BoardWidth { get; set; }

        [DataMember]
        public SimulationSerializer[] Simulations { get; set; }
    }
}
