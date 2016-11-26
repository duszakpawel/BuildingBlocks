using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Exceptions;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Models;
using NUnit.Framework;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    /// Tests for block logic provider class
    /// </summary>
    [TestFixture]
    public class BlockLogicProviderTests
    {
        private readonly IBlockLogicProvider _blocksLogicProvider = new BlockLogicProvider();

        /// <summary>
        ///     Rotate block method test for sample correct block
        /// </summary>
        [Test]
        public void RotateBlockTest_ForSampleCorrectBlock()
        {
            var block = new Block()
            {
                Content = new[,]
                {
                    { true, true },
                    { false, true }
                },
                Width = 2,
                Height = 2
            };

            var result = _blocksLogicProvider.RotateBlock(block);

            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[0].Width, 2);
            Assert.AreEqual(result[0].Height, 2);
            Assert.AreEqual(result[1].Width, 2);
            Assert.AreEqual(result[1].Height, 2);
            Assert.AreEqual(result[2].Width, 2);
            Assert.AreEqual(result[2].Height, 2);
            Assert.AreEqual(result[3].Width, 2);
            Assert.AreEqual(result[3].Height, 2);
            Assert.AreEqual(result[0].Content[0, 0], true);
            Assert.AreEqual(result[0].Content[0, 1], true);
            Assert.AreEqual(result[0].Content[1, 0], false);
            Assert.AreEqual(result[0].Content[1, 1], true);
            Assert.AreEqual(result[1].Content[0, 0], true);
            Assert.AreEqual(result[1].Content[0, 1], true);
            Assert.AreEqual(result[1].Content[1, 0], true);
            Assert.AreEqual(result[1].Content[1, 1], false);
            Assert.AreEqual(result[2].Content[0, 0], true);
            Assert.AreEqual(result[2].Content[0, 1], false);
            Assert.AreEqual(result[2].Content[1, 0], true);
            Assert.AreEqual(result[2].Content[1, 1], true);
            Assert.AreEqual(result[3].Content[0, 0], false);
            Assert.AreEqual(result[3].Content[0, 1], true);
            Assert.AreEqual(result[3].Content[1, 0], true);
            Assert.AreEqual(result[3].Content[1, 1], true);
        }

        /// <summary>
        ///     Rotate block method test for sample correct block
        /// </summary>
        [Test]
        public void RotateBlockTest_ForSampleCorrectBlock2()
        {
            var block = new Block()
            {
                Content = new[,]
                {
                    { true, true }
                },
                Width = 2,
                Height = 1
            };

            var result = _blocksLogicProvider.RotateBlock(block);

            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[0].Width, 2);
            Assert.AreEqual(result[0].Height, 1);
            Assert.AreEqual(result[1].Width, 1);
            Assert.AreEqual(result[1].Height, 2);
            Assert.AreEqual(result[2].Width, 2);
            Assert.AreEqual(result[2].Height, 1);
            Assert.AreEqual(result[3].Width, 1);
            Assert.AreEqual(result[3].Height, 2);
            Assert.AreEqual(result[0].Content[0, 0], true);
            Assert.AreEqual(result[0].Content[0, 1], true);
            Assert.AreEqual(result[1].Content[0, 0], true);
            Assert.AreEqual(result[1].Content[1, 0], true);
            Assert.AreEqual(result[2].Content[0, 0], true);
            Assert.AreEqual(result[2].Content[0, 1], true);
            Assert.AreEqual(result[3].Content[0, 0], true);
            Assert.AreEqual(result[3].Content[1, 0], true);
        }

        /// <summary>
        ///     Rotate block method test for not null content
        /// </summary>
        [Test]
        public void RotateBlockTest_ForNotCorrectBlock()
        {
            var block = new Block()
            {
                Content = null,
                Width = 2,
                Height = 1
            };

            Assert.That(() =>_blocksLogicProvider.RotateBlock(block), Throws.TypeOf<BlockLogicException>());
        }

        /// <summary>
        ///     Rotate block method test for not not correct content (not corresponding to block width and height values)
        /// </summary>
        [Test]
        public void RotateBlockTest_ForNotCorrectBlockContent()
        {
            var block = new Block()
            {
                Content = new[,]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true }
                },
                Width = 2,
                Height = 1
            };

            Assert.That(() => _blocksLogicProvider.RotateBlock(block), Throws.TypeOf<BlockLogicException>());
        }

        /// <summary>
        ///     Find best places for block test for correct values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectValues()
        {
            var board = new[,]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0}
            };

            var content = new[,]
            {
                {true, true, true},
                {true, true, true},
                {true, true, true}
            };

            var result =_blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First().Item1, 0);
            Assert.AreEqual(result.First().Item2, 0);
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectMoreComplexValues()
        {
            var board = new[,]
            {
                {1, 0, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var content = new[,]
            {
                {true},
                {true}
            };

            var result = _blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First().Item1, 0);
            Assert.AreEqual(result.First().Item2, 1);
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectMoreComplexValues2()
        {
            var board = new[,]
            {
                {0, 0, 0},
                {1, 0, 0},
                {1, 1, 1}
            };

            var content = new[,]
            {
                {true},
                {true}
            };

            var result = _blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Item1, 0);
            Assert.AreEqual(result[0].Item2, 2);
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectMoreComplexValues3()
        {
            var board = new[,]
            {
                {1, 0, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var content = new[,]
            {
                {true},
                {true}
            };

            var result = _blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Item1, 0);
            Assert.AreEqual(result[0].Item2, 1);
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values no solution expected exception
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectMoreComplexValues_NoSolution()
        {
            var board = new[,]
            {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            };

            var content = new[,]
            {
                {true},
                {true}
            };

            Assert.That(() => _blocksLogicProvider.FindBestPlacesForBlock(board, content), Throws.TypeOf<BlockLogicException>());
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectComplexValues()
        {
            var board = new[,]
            {
                {1, 1, 1, 2, 2},
                {1, 0, 0, 0, 2},
                {1, 1, 1, 2, 2}
            };

            var content = new[,]
            {
                {true, true, true}
            };

            var result = _blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Item1, 1);
            Assert.AreEqual(result[0].Item2, 1);
        }

        /// <summary>
        ///     Find best places for block test for correct but more complex values
        /// </summary>
        [Test]
        public void FindBestPlacesForBlockTest_ForCorrectComplexValues2()
        {
            var board = new[,]
            {
                {1, 1, 1, 0, 2},
                {1, 0, 0, 0, 2},
                {1, 1, 1, 0, 2}
            };

            var content = new[,]
            {
                {false, true },
                {true , true },
                {false, true }
            };

            var result = _blocksLogicProvider.FindBestPlacesForBlock(board, content);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Item1, 0);
            Assert.AreEqual(result[0].Item2, 2);
        }

        /// <summary>
        ///     Find best places for block test for null content
        /// </summary>
        [Test]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void FindBestPlacesForBlock_ForNotCorrectBoard()
        {
            int[,] board = null;

            var content = new[,]
            {
                {false, true },
                {true , true },
                {false, true }
            };

            Assert.That(() => _blocksLogicProvider.FindBestPlacesForBlock(board, content), Throws.TypeOf<BlockLogicException>());
        }
    }
}