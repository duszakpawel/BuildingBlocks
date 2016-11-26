using System.Collections.Generic;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.BusinessLogic.Parsing;
using BuildingBlocks.Models.Constants;
using BuildingBlocks.Models.Models;
using NUnit.Framework;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    ///     Tests for blocks preprocessor class.
    /// </summary>
    [TestFixture]
    public class BlocksPreprocessorTests
    {
        private readonly IBlocksPreprocessor _blocksPreprocessor = new BlocksPreprocessor();

        /// <summary>
        ///     Preprocess test for sample correct data
        /// </summary>
        [Test]
        public void PreprocessTest_ForSampleCorrectData()
        {
            const int canvasWidth = 100;
            var blocks = new List<Block>
            {
                new Block
                {
                    Width = 2,
                    Height = 3,
                    Content = new[,] { {false, true}, {false, true}, {true, true} }
                }
            };

            var result = _blocksPreprocessor.Preprocess(blocks, canvasWidth);
            Assert.AreEqual(result[0].CanvasChildren.Count, 4);
            Assert.AreEqual(result[0].CanvasChildren[0].FillColor, Constants.BlockFillColor);
            Assert.AreEqual(result[0].CanvasChildren[1].FillColor, Constants.BlockFillColor);
            Assert.AreEqual(result[0].CanvasChildren[0].StrokeColor, Constants.BlockEdgeColor);
            Assert.AreEqual(result[0].CanvasChildren[1].StrokeColor, Constants.BlockEdgeColor);
            Assert.AreEqual(result[0].CanvasChildren[0].Width, 33);
            Assert.AreEqual(result[0].CanvasChildren[0].Height, 33);
            Assert.AreEqual(result[0].CanvasChildren[1].Width, 33);
            Assert.AreEqual(result[0].CanvasChildren[1].Height, 33);
            Assert.AreEqual(result[0].CanvasChildren[0].X, 33);
            Assert.AreEqual(result[0].CanvasChildren[0].Y, 0);
            Assert.AreEqual(result[0].CanvasChildren[1].X, 33);
            Assert.AreEqual(result[0].CanvasChildren[1].Y, 33);
        }

        /// <summary>
        ///     Preprocess test for sample correct data
        /// </summary>
        [Test]
        public void PreprocessTest_ForSampleCorrectData2()
        {
            const int canvasWidth = 100;
            var blocks = new List<Block>
            {
                new Block
                {
                    Width = 2,
                    Height = 2,
                    Content = new[,] { {false, true}, {false, true} }
                }
            };

            var result = _blocksPreprocessor.Preprocess(blocks, canvasWidth);
            Assert.AreEqual(result[0].CanvasChildren.Count, 2);
            Assert.AreEqual(result[0].CanvasChildren[0].FillColor, Constants.BlockFillColor);
            Assert.AreEqual(result[0].CanvasChildren[1].FillColor, Constants.BlockFillColor);
            Assert.AreEqual(result[0].CanvasChildren[0].StrokeColor, Constants.BlockEdgeColor);
            Assert.AreEqual(result[0].CanvasChildren[1].StrokeColor, Constants.BlockEdgeColor);
            Assert.AreEqual(result[0].CanvasChildren[0].Width, 50);
            Assert.AreEqual(result[0].CanvasChildren[0].Height, 50);
            Assert.AreEqual(result[0].CanvasChildren[1].Width, 50);
            Assert.AreEqual(result[0].CanvasChildren[1].Height, 50);
            Assert.AreEqual(result[0].CanvasChildren[0].X, 50);
            Assert.AreEqual(result[0].CanvasChildren[0].Y, 0);
            Assert.AreEqual(result[0].CanvasChildren[1].X, 50);
            Assert.AreEqual(result[0].CanvasChildren[1].Y, 50);
        }
    }
}