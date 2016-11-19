using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models
{
    public class Simulation : INotifyPropertyChanged
    {
        public ObservableCollection<RectItem> CanvasChildren { get; set; } = new ObservableCollection<RectItem>();

        public List<Block> AvailableBlocks { get; set; } = new List<Block>();

        public int WellHeight { get; set; }

        public bool[,] Content { get; set; }

        public bool[,] LastBlock { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
