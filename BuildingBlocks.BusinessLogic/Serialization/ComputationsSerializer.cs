using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;
using BuildingBlocks.Models.Serializable;

namespace BuildingBlocks.BusinessLogic.Serialization
{
    /// <summary>
    ///     Serializer class for computations data.
    /// </summary>
    public class ComputationsSerializer : IComputationsSerializer
    {
        /// <summary>
        ///     Stores computations data in XML file with specified name.
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <param name="boardWidth">Board width</param>
        /// <param name="K">K parameter</param>
        /// <param name="simulations">Simulations collection</param>
        public void Serialize(string filename, int boardWidth, int K, IEnumerable<Simulation> simulations)
        {
            try
            {
                var computationsData = new ComputationsData
                {
                    BoardWidth = boardWidth,
                    K = K,
                    Simulations = new SimulationData[K]
                };

                var i = 0;

                foreach (var item in simulations)
                {
                    computationsData.Simulations[i] = new SimulationData
                    {
                        WellHeight = item.WellHeight,
                        CanvasChildren = new RectItemData[item.CanvasChildren.Count],
                        AvailableBlocks = new BlockData[item.AvailableBlocks.Count],
                        Content = new int[item.Content.Length],
                        LastBlock = new int[item.LastBlock.Length],
                        Score = item.Score,
                        Height = item.Height
                    };

                    for (var k = 0; k < boardWidth; ++k)
                    {
                        for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                        {
                            computationsData.Simulations[i].Content[j * boardWidth + k] = item.Content[k, j];
                        }
                    }

                    for (var k = 0; k < boardWidth; ++k)
                    {
                        for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                        {
                            computationsData.Simulations[i].LastBlock[j * boardWidth + k] = item.LastBlock[k, j];
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
                                computationsData.Simulations[i].AvailableBlocks[j].Content[
                                    m * item.AvailableBlocks[j].Width + k] = item.AvailableBlocks[j].Content[m, k];
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

                var dcSerializer = new DataContractSerializer(typeof(ComputationsData));

                using (var xdWriter = XmlDictionaryWriter.CreateTextWriter(File.Open(filename, FileMode.Create), Encoding.UTF8))
                {
                    dcSerializer.WriteObject(xdWriter, computationsData);
                }
            }
            catch (Exception)
            {
                throw new SerializationException();
            }
        }

        /// <summary>
        ///     Deserializes computations data from specified XML file.
        /// </summary>
        /// <param name="filename">Name of XML file</param>
        /// <returns>BoardWidth, K, Simulations</returns>
        public Tuple<int, int, IEnumerable<Simulation>> Deserialize(string filename)
        {
            try
            {
                var dcs = new DataContractSerializer(typeof(ComputationsData));
                ComputationsData computationsData;
                using (var fs = new FileStream(filename, FileMode.Open))
                using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                {
                    computationsData = (ComputationsData)dcs.ReadObject(reader);
                }
                var boardWidth = computationsData.BoardWidth;
                var k = computationsData.K;
                var simulations = new ObservableCollection<Simulation>();

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

                    var content = new int[boardWidth, item.WellHeight / Constants.SingleTileWidth];

                    for (var i = 0; i < boardWidth; ++i)
                    {
                        for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                        {
                            content[i, j] = item.Content[j * boardWidth + i];
                        }
                    }

                    var lastBlock = new int[boardWidth, item.WellHeight / Constants.SingleTileWidth];
                    for (var i = 0; i < boardWidth; ++i)
                    {
                        for (var j = 0; j < item.WellHeight / Constants.SingleTileWidth; ++j)
                        {
                            lastBlock[i, j] = item.LastBlock[j * boardWidth + i];
                        }
                    }

                    simulations.Add(new Simulation
                    {
                        WellHeight = item.WellHeight,
                        AvailableBlocks = blocks,
                        Content = content,
                        LastBlock = lastBlock,
                        CanvasChildren = ris,
                        Height = item.Height,
                        Score = item.Score
                    });
                }

                return new Tuple<int, int, IEnumerable<Simulation>>(boardWidth, k, simulations);
            }
            catch (Exception)
            {
                throw new SerializationException();
            }
        }
    }
}