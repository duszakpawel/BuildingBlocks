using System.Runtime.Serialization;

namespace BuildingBlocks.Models.Serializable
{
    /// <summary>
    ///     Rectangle item data
    /// </summary>
    [DataContract]
    public class RectItemData
    {
        /// <summary>
        ///     X - coordinate
        /// </summary>
        [DataMember]
        public int X { get; set; }

        /// <summary>
        ///     Y - coordinate
        /// </summary>
        [DataMember]
        public int Y { get; set; }

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
    }
}