﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using BuildingBlocks.Models.Annotations;

namespace BuildingBlocks.Models.Models
{
    /// <summary>
    /// Rectangle item class
    /// </summary>
    public class RectItem : INotifyPropertyChanged
    {
        /// <summary>
        /// X- coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y - coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Fill color
        /// </summary>
        public Brush FillColor { get; set; }

        /// <summary>
        /// Stroke color
        /// </summary>
        public Brush StrokeColor { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RectItem(){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        public RectItem(int x, int y)
        {
            X = x;
            Y = y;
            Height = Constants.Constants.SingleTileWidth;
            Width = Constants.Constants.SingleTileWidth;
            FillColor = Constants.Constants.OldBlockEdgeColor;
            StrokeColor = Constants.Constants.BlockEdgeColor;
        }

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed event handler
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}