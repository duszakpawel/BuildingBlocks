using System.Collections.Generic;
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

        private const int rectSize = 20;

        public BlocksBrowserViewModel(List<Block> blocks)
        {
            Blocks = blocks;

            foreach (var block in Blocks)
            {
                block.CanvasChildren = new List<Rectangle>();
                for (int i = 0; i < block.Heigth; i++)
                {
                    for (int j = 0; j < block.Width; j++)
                    {
                        if (block.Content[i, j])
                        {
                            Rectangle rect = new Rectangle();
                            rect.Fill = Brushes.CornflowerBlue;
                            rect.Width = rectSize;
                            rect.Height = rectSize;
                            rect.Stroke = Brushes.DarkCyan;
                            Canvas.SetTop(rect, i* rectSize);
                            Canvas.SetLeft(rect, j* rectSize);
                            block.CanvasChildren.Add(rect);
                        }
                    }
                }
            }
        }

    }
}