using System.Runtime.Serialization;

namespace BuildingBlocks.Models.Serializable
{
    /// <summary>
    ///     Simulation data
    /// </summary>
    [DataContract]
    public class SimulationData
    {
        /// <summary>
        ///     Canvas children collection (rectangles)
        /// </summary>
        [DataMember]
        public RectItemData[] CanvasChildren { get; set; }

        /// <summary>
        ///     Available blocks collection
        /// </summary>
        [DataMember]
        public BlockData[] AvailableBlocks { get; set; }

        /// <summary>
        ///     Well width
        /// </summary>
        [DataMember]
        public int WellHeight { get; set; }

        /// <summary>
        ///     Content array
        /// </summary>
        [DataMember]
        public int[] Content { get; set; }

        /// <summary>
        ///     Last block content array
        /// </summary>
        [DataMember]
        public int[] LastBlock { get; set; }

        /// <summary>
        ///     Score
        /// </summary>
        [DataMember]
        public int Score { get; set; }

        /// <summary>
        ///     Height
        /// </summary>
        [DataMember]
        public int Height { get; set; }
    }
}