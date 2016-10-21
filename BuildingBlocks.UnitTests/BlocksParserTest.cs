using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingBlocks.BusinessLogic;

namespace BuildingBlocks.UnitTests
{
    [TestClass]
    public class BlocksParserTest
    {
        [TestMethod]
        public void SampleFileParse()
        {
            const string text = @"8 2
2 3
0 1
0 1
1 1
4 3
1 0 0 1
1 0 0 1
1 1 1 1";
            var file = new StringReader(text);
            var parser = new BlocksParser();
            var result = parser.LoadData(file);
            Assert.AreEqual(result.WellWidth, 8);
            Assert.AreEqual(result.BlocksCount, 2);
            Assert.AreEqual(result.Blocks.Count, 2);
            Assert.AreEqual(result.Blocks[0].Width, 2);
            Assert.AreEqual(result.Blocks[0].Height, 3);
            Assert.AreEqual(result.Blocks[0].Content[0, 0], false);
            Assert.AreEqual(result.Blocks[0].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[1, 0], false);
            Assert.AreEqual(result.Blocks[0].Content[1, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[1].Width, 4);
            Assert.AreEqual(result.Blocks[1].Height, 3);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], false);
            Assert.AreEqual(result.Blocks[1].Content[0, 2], false);
            Assert.AreEqual(result.Blocks[1].Content[0, 3], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], false);
            Assert.AreEqual(result.Blocks[1].Content[1, 2], false);
            Assert.AreEqual(result.Blocks[1].Content[1, 3], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 2], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 3], true);
        }
    }
}
