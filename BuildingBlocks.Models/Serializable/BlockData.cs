using System.Runtime.Serialization;

namespace BuildingBlocks.Models.Serializable
{
    /// <summary>
    ///     Block data
    /// </summary>
    [DataContract]
    public class BlockData
    {
        /// <summary>
        ///     Width
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        ///     Height
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        ///     Content array
        /// </summary>
        [DataMember]
        public bool[] Content { get; set; }

        /// <summary>
        ///     Canvas children (RectItemSerializer collection)
        /// </summary>
        [DataMember]
        public RectItemData[] CanvasChildren { get; set; }

        /// <summary>
        ///     Quantity
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }

        /// <summary>
        ///     IsQuantityEnabled
        /// </summary>
        [DataMember]
        public bool IsQuantityEnabled { get; set; }
    }
}