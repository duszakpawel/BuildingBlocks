using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models.Models
{
    /// <summary>
    /// Block class
    /// </summary>
    public class Block : INotifyPropertyChanged
    {
        private static int idCounter = 0;

        /// <summary>
        /// Block Id
        /// </summary>
        public int Id;

        /// <summary>
        /// Block width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Block height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Block array content
        /// </summary>
        public bool[,] Content { get; set; }

        /// <summary>
        /// Rectangle collection representing tiles
        /// </summary>
        public List<RectItem> CanvasChildren { get; set; } = new List<RectItem>();

        /// <summary>
        /// Block quantity
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Returns information whether quantity manipulation is enabled or disabled for this block
        /// </summary>
        public bool IsQuantityEnabled { get; set; }

        /// <summary>
        /// Event required by INotifyPropertyChanged interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// constructor
        /// </summary>
        public Block()
        {
            Id = idCounter++;
        }

        /// <summary>
        /// Copying constructor
        /// </summary>
        /// <param name="block">Another block</param>
        public Block(Block block)
        {
            Id = block.Id;
            Width = block.Width;
            Height = block.Height;
            Content = (bool[,]) block.Content.Clone();
            Quantity = block.Quantity;
        }

        /// <summary>
        /// property changed event handler
        /// </summary>
        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
