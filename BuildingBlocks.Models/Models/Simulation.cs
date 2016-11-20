﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models.Models
{
    /// <summary>
    /// Simulation class
    /// </summary>
    public class Simulation : INotifyPropertyChanged
    {
        /// <summary>
        /// Rectangles representing blocks
        /// </summary>
        public ObservableCollection<RectItem> CanvasChildren { get; set; } = new ObservableCollection<RectItem>();

        /// <summary>
        /// Available blocks to add to simulation
        /// </summary>
        public List<Block> AvailableBlocks { get; set; } = new List<Block>();

        /// <summary>
        /// Well width
        /// </summary>
        public int WellHeight { get; set; }

        /// <summary>
        /// Simulation content aray
        /// </summary>
        public bool[,] Content { get; set; }

        /// <summary>
        /// Last block content array
        /// </summary>
        public int[,] LastBlock { get; set; }

        private int _score = 0;
        /// <summary>
        /// Simulation score
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private int _height = 0;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        /// <summary>
        /// Property changed event, required by INotiftyPropertyChanged interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
