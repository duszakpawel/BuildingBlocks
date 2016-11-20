using System.Runtime.Serialization;

namespace BuildingBlocks.Models.Serializable
{
    /// <summary>
    ///     Computations data
    /// </summary>
    [DataContract]
    public class ComputationsData
    {
        /// <summary>
        ///     K parameter
        /// </summary>
        [DataMember]
        public int K { get; set; }

        /// <summary>
        ///     Board width
        /// </summary>
        [DataMember]
        public int BoardWidth { get; set; }

        /// <summary>
        ///     Simulations
        /// </summary>
        [DataMember]
        public SimulationData[] Simulations { get; set; }
    }
}