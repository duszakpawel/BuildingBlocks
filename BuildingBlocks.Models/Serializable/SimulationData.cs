using BuildingBlocks.Models.Models;
using System.Runtime.Serialization;

namespace BuildingBlocks.Models.Models
{
    /// <summary>
    /// Simulation data
    /// </summary>
    [DataContract]
    public class SimulationData
    {
        /// <summary>
        /// Canvas children collection (rectangles)
        /// </summary>
        [DataMember]
        public RectItemData[] CanvasChildren { get; set; } 

        /// <summary>
        /// Available blocks collection
        /// </summary>
        [DataMember]
        public BlockData[] AvailableBlocks { get; set; }

        /// <summary>
        /// Well width
        /// </summary>
        [DataMember]
        public int WellHeight { get; set; }

        /// <summary>
        /// Content array
        /// </summary>
        [DataMember]
        public bool[] Content { get; set; }

        /// <summary>
        /// Last block content array
        /// </summary>
        [DataMember]
        public bool[] LastBlock { get; set; }
    }
}
