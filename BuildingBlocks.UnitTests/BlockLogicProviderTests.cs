using BuildingBlocks.BusinessLogic.Algorithm;
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
    }
}