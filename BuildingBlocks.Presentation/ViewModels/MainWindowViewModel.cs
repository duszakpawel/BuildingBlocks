using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using BuildingBlocks.BusinessLogic;
using BuildingBlocks.Presentation.Common;
using Caliburn.Micro;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using BuildingBlocks.Models;

namespace BuildingBlocks.Presentation.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public BlocksBrowserViewModel BlocksBrowserViewViewModel { get; set; }

        public AlgorithmSimulationViewModel AlgorithmSimulationViewViewModel { get; set; }

        public int BoardWidth { get; set; }

        public int K { get; set; } = 1;

        public int Step { get; set; } = 1;

        public bool CanStart => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        public bool CanStop { get; set; }

        public bool CanPause { get; set; }

        public bool CanNext => K > 0 && Step > 0 && BoardWidth > 0 && !IsProcessing;

        public bool CanLoadFile { get; set; } = true;

        public bool CanSave => AlgorithmSimulationViewViewModel != null;

        public bool CanLoad => AlgorithmSimulationViewViewModel == null;

        public bool IsProcessing { get; set; }

        public bool IsExpanded { get; set; } = true;

        public bool IsKEnabled { get; set; } = true;

        public bool IsStepEnabled { get; set; } = true;

        public async void LoadFile(string name)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt"
            };
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }
            var blocks = await new BlocksParser().LoadData(new StreamReader(openFileDialog.FileName));
            BlocksBrowserViewViewModel = new BlocksBrowserViewModel(blocks.Blocks);
            BoardWidth = blocks.WellWidth;
        }

        public void Start(string name)
        {
            IsProcessing = true;
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = true;
            IsKEnabled = false;
            IsStepEnabled = false;
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.Selected);
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Start(Step);
        }

        public void Stop(string name)
        {
            IsProcessing = false;
            CanLoadFile = true;
            IsExpanded = true;
            CanStop = false;
            CanPause = false;
            IsKEnabled = true;
            IsStepEnabled = true;
            BlocksBrowserViewViewModel.UpdateBrowserView(DisplayMode.All);
            AlgorithmSimulationViewViewModel.Stop();
            AlgorithmSimulationViewViewModel = null;
            BlocksBrowserViewViewModel.EnableQuantity();
        }

        public void Pause(string name)
        {
            IsProcessing = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            AlgorithmSimulationViewViewModel.Pause();
        }

        public void Next(string name)
        {
            CanLoadFile = false;
            IsExpanded = false;
            CanStop = true;
            CanPause = false;
            IsKEnabled = false;
            IsStepEnabled = true;
            if (AlgorithmSimulationViewViewModel == null)
            {
                AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(BlocksBrowserViewViewModel.DisplayedBlocks, BoardWidth, K, Step);
                BlocksBrowserViewViewModel.DisableQuantity();
            }
            AlgorithmSimulationViewViewModel.Next(Step);
        }

        public void Save(string name)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = "xml files (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }
            var a = new AlgorithmSerializer
            {
                BoardWidth = BoardWidth,
                K = K,
                Simulations = new SimulationSerializer[K]
            };
            var i = 0;
            foreach (var item in AlgorithmSimulationViewViewModel.Simulations)
            {
                a.Simulations[i] = new SimulationSerializer
                {
                    WellHeight = item.WellHeight,
                    CanvasChildren = new RectItemSerializer[item.CanvasChildren.Count],
                    AvailableBlocks = new BlockSerializer[item.AvailableBlocks.Count],
                    Content = new bool[item.Content.Length],
                    LastBlock = new bool[item.LastBlock.Length]
                };
                for (var k = 0; k < BoardWidth; ++k)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        a.Simulations[i].Content[j * BoardWidth + k] = item.Content[k, j];
                    }
                }
                for (var k = 0; k < BoardWidth; ++k)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        a.Simulations[i].LastBlock[j * BoardWidth + k] = item.LastBlock[k, j];
                    }
                }
                for (var j = 0; j < item.CanvasChildren.Count; ++j)
                {
                    a.Simulations[i].CanvasChildren[j] = new RectItemSerializer
                    {
                        Width = item.CanvasChildren[j].Width,
                        Height = item.CanvasChildren[j].Height,
                        X = item.CanvasChildren[j].X,
                        Y = item.CanvasChildren[j].Y
                    };
                }
                for (var j = 0; j < item.AvailableBlocks.Count; ++j)
                {
                    a.Simulations[i].AvailableBlocks[j] = new BlockSerializer
                    {
                        Width = item.AvailableBlocks[j].Width,
                        Height = item.AvailableBlocks[j].Height,
                        IsQuantityEnabled = item.AvailableBlocks[j].IsQuantityEnabled,
                        Quantity = item.AvailableBlocks[j].Quantity,
                        CanvasChildren = new RectItemSerializer[item.AvailableBlocks[j].CanvasChildren.Count],
                        Content = new bool[item.AvailableBlocks[j].Content.Length]
                    };
                    for (var k = 0; k < item.AvailableBlocks[j].Width; ++k)
                    {
                        for (var m = 0; m < item.AvailableBlocks[j].Height; ++m)
                        {
                            a.Simulations[i].AvailableBlocks[j].Content[m * item.AvailableBlocks[j].Width + k] = item.AvailableBlocks[j].Content[m, k];
                        }
                    }
                    for (var k = 0; k < item.AvailableBlocks[j].CanvasChildren.Count; ++k)
                    {
                        a.Simulations[i].AvailableBlocks[j].CanvasChildren[k] = new RectItemSerializer
                        {
                            Height = item.AvailableBlocks[j].CanvasChildren[k].Height,
                            Width = item.AvailableBlocks[j].CanvasChildren[k].Width,
                            Y = item.AvailableBlocks[j].CanvasChildren[k].Y,
                            X = item.AvailableBlocks[j].CanvasChildren[k].X
                        };
                    }
                }
                ++i;
            }
            var dcs = new DataContractSerializer(typeof(AlgorithmSerializer));
            using (var xdw = XmlDictionaryWriter.CreateTextWriter(File.Open(openFileDialog.FileName, FileMode.Create), Encoding.UTF8))
                dcs.WriteObject(xdw, a);
        }

        public void Load(string name)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "xml files (*.xml)|*.xml"
            };
            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }
            var dcs = new DataContractSerializer(typeof(AlgorithmSerializer));
            AlgorithmSerializer a;
            using (var fs = new FileStream(openFileDialog.FileName, FileMode.Open))
            using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
            {
                a = (AlgorithmSerializer)dcs.ReadObject(reader);
            }
            AlgorithmSimulationViewViewModel = new AlgorithmSimulationViewModel(new List<Block>(), a.BoardWidth, a.K, Step);
            BoardWidth = a.BoardWidth;
            K = a.K;
            AlgorithmSimulationViewViewModel.Simulations = new ObservableCollection<Simulation>();
            foreach (var item in a.Simulations)
            {
                var abs = new List<Block>();
                foreach (var ab in item.AvailableBlocks)
                {
                    var cc = ab.CanvasChildren.Select(ri => new RectItem
                    {
                        Width = ri.Width,
                        Height = ri.Height,
                        Y = ri.Y,
                        X = ri.X,
                        FillColor = Constants.BlockFillColor,
                        StrokeColor = Constants.BlockEdgeColor
                    }).ToList();
                    var c = new bool[ab.Height, ab.Width];
                    for (var i = 0; i < ab.Height; ++i)
                    {
                        for (var j = 0; j < ab.Width; ++j)
                        {
                            c[i, j] = ab.Content[i * ab.Width + j];
                        }
                    }
                    abs.Add(new Block
                    {
                        IsQuantityEnabled = ab.IsQuantityEnabled,
                        Height = ab.Height,
                        Quantity = ab.Quantity,
                        Width = ab.Width,
                        Content = c,
                        CanvasChildren = cc
                    });
                }
                var ris = new ObservableCollection<RectItem>();
                foreach (var ri in item.CanvasChildren)
                {
                    ris.Add(new RectItem
                    {
                        Width = ri.Width,
                        Height = ri.Height,
                        Y = ri.Y,
                        X = ri.X,
                        FillColor = Constants.BlockFillColor,
                        StrokeColor = Constants.BlockEdgeColor
                    });
                }
                var content = new bool[BoardWidth, item.WellHeight / Constants.SingleTileWidth];
                for (var i = 0; i < BoardWidth; ++i)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        content[i, j] = item.Content[j * BoardWidth + i];
                    }
                }
                var lastBlock = new bool[BoardWidth, item.WellHeight / Constants.SingleTileWidth];
                for (var i = 0; i < BoardWidth; ++i)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        lastBlock[i, j] = item.LastBlock[j * BoardWidth + i];
                    }
                }
                AlgorithmSimulationViewViewModel.Simulations.Add(new Simulation
                {
                    WellHeight = item.WellHeight,
                    AvailableBlocks = abs,
                    Content = content,
                    LastBlock = lastBlock,
                    CanvasChildren = ris
                });
            }
            Start(name);
            Pause(name);
        }
    }
}