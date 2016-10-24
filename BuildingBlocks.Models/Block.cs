using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class Block : INotifyPropertyChanged
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool[,] Content { get; set; }

        public List<Rectangle> CanvasChildren { get; set; } = new List<Rectangle>();

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= 0) _quantity = value;
            }
        }

        public bool IsQuantityEnabled
        {
            get { return _isquantityenabled; }
            set
            {
                _isquantityenabled = value;
                OnPropertyChanged(nameof(IsQuantityEnabled));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _quantity;

        private bool _isquantityenabled = true;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
