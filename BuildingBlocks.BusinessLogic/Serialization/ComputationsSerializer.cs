using BuildingBlocks.Models;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Models.Constants;

namespace BuildingBlocks.BusinessLogic
{
    /// <summary>
    /// Serializer class for computations data.
    /// </summary>
    public class ComputationsSerializer
    {
        /// <summary>
        /// Stores computations data in XML file with specified name.
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <param name="BoardWidth">Board width</param>
        /// <param name="K">K parameter</param>
        /// <param name="Simulations">Simulations collection</param>
        public void Serialize(string filename, int BoardWidth, int K, IEnumerable<Simulation> Simulations)
        {
            var computationsData = new ComputationsData
            {
                BoardWidth = BoardWidth,
                K = K,
                Simulations = new SimulationData[K]
            };

            var i = 0;

            foreach (var item in Simulations)
            {
                computationsData.Simulations[i] = new SimulationData
                {
                    WellHeight = item.WellHeight,
                    CanvasChildren = new RectItemData[item.CanvasChildren.Count],
                    AvailableBlocks = new BlockData[item.AvailableBlocks.Count],
                    Content = new bool[item.Content.Length],
                    LastBlock = new int[item.LastBlock.Length]
                };

                for (var k = 0; k < BoardWidth; ++k)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        computationsData.Simulations[i].Content[j * BoardWidth + k] = item.Content[k, j];
                    }
                }

                for (var k = 0; k < BoardWidth; ++k)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        computationsData.Simulations[i].LastBlock[j * BoardWidth + k] = item.LastBlock[k, j];
                    }
                }

                for (var j = 0; j < item.CanvasChildren.Count; ++j)
                {
                    computationsData.Simulations[i].CanvasChildren[j] = new RectItemData
                    {
                        Width = item.CanvasChildren[j].Width,
                        Height = item.CanvasChildren[j].Height,
                        X = item.CanvasChildren[j].X,
                        Y = item.CanvasChildren[j].Y
                    };
                }

                for (var j = 0; j < item.AvailableBlocks.Count; ++j)
                {
                    computationsData.Simulations[i].AvailableBlocks[j] = new BlockData
                    {
                        Width = item.AvailableBlocks[j].Width,
                        Height = item.AvailableBlocks[j].Height,
                        IsQuantityEnabled = item.AvailableBlocks[j].IsQuantityEnabled,
                        Quantity = item.AvailableBlocks[j].Quantity,
                        CanvasChildren = new RectItemData[item.AvailableBlocks[j].CanvasChildren.Count],
                        Content = new bool[item.AvailableBlocks[j].Content.Length]
                    };

                    for (var k = 0; k < item.AvailableBlocks[j].Width; ++k)
                    {
                        for (var m = 0; m < item.AvailableBlocks[j].Height; ++m)
                        {
                            computationsData.Simulations[i].AvailableBlocks[j].Content[m * item.AvailableBlocks[j].Width + k] = item.AvailableBlocks[j].Content[m, k];
                        }
                    }

                    for (var k = 0; k < item.AvailableBlocks[j].CanvasChildren.Count; ++k)
                    {
                        computationsData.Simulations[i].AvailableBlocks[j].CanvasChildren[k] = new RectItemData
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

            var dcSerializer = new  DataContractSerializer(typeof(ComputationsData));

            using (var xdWriter = XmlDictionaryWriter.CreateTextWriter(File.Open(filename, FileMode.Create), Encoding.UTF8))
            {
                dcSerializer.WriteObject(xdWriter, computationsData);
            }
        }

        /// <summary>
        /// Deserializes computations data from specified XML file.
        /// </summary>
        /// <param name="filename">Name of XML file</param>
        /// <returns></returns>
        public Tuple<int, int, IEnumerable<Simulation>> Deserialize(string filename)
        {
            var dcs = new DataContractSerializer(typeof(ComputationsData));
            ComputationsData computationsData;
            using (var fs = new FileStream(filename, FileMode.Open))
            using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
            {
                computationsData = (ComputationsData)dcs.ReadObject(reader);
            }
            var BoardWidth = computationsData.BoardWidth;
            var K = computationsData.K;
            var Simulations = new ObservableCollection<Simulation>();

            foreach (var item in computationsData.Simulations)
            {
                var blocks = new List<Block>();

                foreach (var availableBlock in item.AvailableBlocks)
                {
                    var canvasChildrens = availableBlock.CanvasChildren.Select(rect => new RectItem
                    {
                        Width = rect.Width,
                        Height = rect.Height,
                        Y = rect.Y,
                        X = rect.X,
                        FillColor = Constants.BlockFillColor,
                        StrokeColor = Constants.BlockEdgeColor
                    }).ToList();

                    var blockContent = new bool[availableBlock.Height, availableBlock.Width];

                    for (var i = 0; i < availableBlock.Height; ++i)
                    {
                        for (var j = 0; j < availableBlock.Width; ++j)
                        {
                            blockContent[i, j] = availableBlock.Content[i * availableBlock.Width + j];
                        }
                    }

                    blocks.Add(new Block
                    {
                        IsQuantityEnabled = availableBlock.IsQuantityEnabled,
                        Height = availableBlock.Height,
                        Quantity = availableBlock.Quantity,
                        Width = availableBlock.Width,
                        Content = blockContent,
                        CanvasChildren = canvasChildrens
                    });
                }

                var ris = new ObservableCollection<RectItem>();

                foreach (var rect in item.CanvasChildren)
                {
                    ris.Add(new RectItem
                    {
                        Width = rect.Width,
                        Height = rect.Height,
                        Y = rect.Y,
                        X = rect.X,
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

                var lastBlock = new int[BoardWidth, item.WellHeight / Constants.SingleTileWidth];
                for (var i = 0; i < BoardWidth; ++i)
                {
                    for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                    {
                        lastBlock[i, j] = item.LastBlock[j * BoardWidth + i];
                    }
                }

                Simulations.Add(new Simulation
                {
                    WellHeight = item.WellHeight,
                    AvailableBlocks = blocks,
                    Content = content,
                    LastBlock = lastBlock,
                    CanvasChildren = ris
                });
            }

            return new Tuple<int, int, IEnumerable<Simulation>>(BoardWidth, K, Simulations);
        }
    }
}
