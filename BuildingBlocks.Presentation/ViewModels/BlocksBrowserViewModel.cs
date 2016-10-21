﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BuildingBlocks.Models;
using Caliburn.Micro;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class BlocksBrowserViewModel : Screen
    {
        public List<Block> Blocks { get; set; }

        private const int CanvasWidth = 100;

        public BlocksBrowserViewModel(List<Block> blocks)
        {
            Blocks = blocks;

            var maxWidth = blocks.Max(item => item.Width);
            var maxHeight = blocks.Max(item => item.Height);
            var rectSize = CanvasWidth / (maxHeight > maxWidth ? maxHeight : maxWidth);

            foreach (var block in Blocks)
            {
                block.CanvasChildren = new List<Rectangle>();

                // shift to center block vertically on list
                int rectVerticalShift = (int) ((((double) (maxHeight - block.Height))/2)*rectSize);

                for (var i = 0; i < block.Height; ++i)
                {
                    for (var j = 0; j < block.Width; ++j)
                    {
                        if (!block.Content[i, j]) continue;
                        var rect = new Rectangle
                        {
                            Fill = Brushes.CornflowerBlue,
                            Width = rectSize,
                            Height = rectSize,
                            Stroke = Brushes.DarkCyan
                        };
                        Canvas.SetTop(rect, i * rectSize + rectVerticalShift);
                        Canvas.SetLeft(rect, j * rectSize);
                        block.CanvasChildren.Add(rect);
                    }
                }
            }
        }
    }
}