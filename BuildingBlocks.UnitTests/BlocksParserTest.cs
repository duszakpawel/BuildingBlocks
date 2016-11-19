using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingBlocks.BusinessLogic;
using System.Threading.Tasks;
using System;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    /// Tests for blocks parser logic.
    /// </summary>
    [TestClass]
    public class BlocksParserTest
    {
        private BlocksParser parser = new BlocksParser();

        /// <summary>
        /// Parser test for different widths of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForDifferentWidthBlocks()
        {
            const string text = 
@"8 2
2 3
0 1
0 1
1 1
4 3
1 0 0 1
1 0 0 1
1 1 1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

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

        /// <summary>
        /// Parser test for different widths of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForDifferentWidthBlocks2()
        {
            const string text =
@"8 2
4 3
1 0 0 1
1 0 0 1
1 1 1 1
2 3
0 1
0 1
1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

            Assert.AreEqual(result.WellWidth, 8);
            Assert.AreEqual(result.BlocksCount, 2);
            Assert.AreEqual(result.Blocks.Count, 2);
            Assert.AreEqual(result.Blocks[0].Width, 4);
            Assert.AreEqual(result.Blocks[0].Height, 3);
            Assert.AreEqual(result.Blocks[0].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[0, 1], false);
            Assert.AreEqual(result.Blocks[0].Content[0, 2], false);
            Assert.AreEqual(result.Blocks[0].Content[0, 3], true);
            Assert.AreEqual(result.Blocks[0].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[1, 1], false);
            Assert.AreEqual(result.Blocks[0].Content[1, 2], false);
            Assert.AreEqual(result.Blocks[0].Content[1, 3], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 2], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 3], true);
            Assert.AreEqual(result.Blocks[1].Width, 2);
            Assert.AreEqual(result.Blocks[1].Height, 3);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], false);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], false);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 1], true);
        }

        /// <summary>
        /// Parser test for different heights of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForDifferentHeightBlocks()
        {
            const string text =
@"8 2
2 4
0 1
0 1
1 1
0 1
2 3
1 0
1 0
1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

            Assert.AreEqual(result.WellWidth, 8);
            Assert.AreEqual(result.BlocksCount, 2);
            Assert.AreEqual(result.Blocks.Count, 2);
            Assert.AreEqual(result.Blocks[0].Width, 2);
            Assert.AreEqual(result.Blocks[0].Height, 4);
            Assert.AreEqual(result.Blocks[0].Content[0, 0], false);
            Assert.AreEqual(result.Blocks[0].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[1, 0], false);
            Assert.AreEqual(result.Blocks[0].Content[1, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[0].Content[3, 0], false);
            Assert.AreEqual(result.Blocks[0].Content[3, 1], true);
            Assert.AreEqual(result.Blocks[1].Width, 2);
            Assert.AreEqual(result.Blocks[1].Height, 3);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], false);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], false);
            Assert.AreEqual(result.Blocks[1].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 1], true);
        }

        /// <summary>
        /// Parser test for different heights of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForDifferentHeightBlocks2()
        {
            const string text =
@"8 2
2 3
1 0
1 0
1 1
2 4
0 1
0 1
1 1
0 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

            Assert.AreEqual(result.WellWidth, 8);
            Assert.AreEqual(result.BlocksCount, 2);
            Assert.AreEqual(result.Blocks.Count, 2);
            Assert.AreEqual(result.Blocks[0].Width, 2);
            Assert.AreEqual(result.Blocks[0].Height, 3);
            Assert.AreEqual(result.Blocks[0].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[0, 1], false);
            Assert.AreEqual(result.Blocks[0].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[1, 1], false);
            Assert.AreEqual(result.Blocks[0].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[0].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[1].Width, 2);
            Assert.AreEqual(result.Blocks[1].Height, 4);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], false);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], false);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[3, 0], false);
            Assert.AreEqual(result.Blocks[1].Content[3, 1], true);
        }

        /// <summary>
        /// Parser test for equal sizes of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForEqualSizeBlocks()
        {
            const string text =
@"8 2
2 3
0 1
0 1
1 1
2 2
1 1
1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

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
            Assert.AreEqual(result.Blocks[1].Width, 2);
            Assert.AreEqual(result.Blocks[1].Height, 2);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], true);
        }

        /// <summary>
        /// Parser test for not equal sizes of blocks.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForNotEqualSizeBlocks()
        {
            const string text =
@"8 2
2 3
0 1
0 1
1 1
3 4
1 1 1
1 1 1
1 1 1
1 1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

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
            Assert.AreEqual(result.Blocks[1].Width, 3);
            Assert.AreEqual(result.Blocks[1].Height, 4);
            Assert.AreEqual(result.Blocks[1].Content[0, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[0, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[0, 2], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[1, 2], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[2, 2], true);
            Assert.AreEqual(result.Blocks[1].Content[3, 0], true);
            Assert.AreEqual(result.Blocks[1].Content[3, 1], true);
            Assert.AreEqual(result.Blocks[1].Content[3, 2], true);
        }

        /// <summary>
        /// Parser test for no blocks in file.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForNoBlocks()
        {
            const string text =
@"8 0";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);

            Assert.AreEqual(result.WellWidth, 8);
            Assert.AreEqual(result.BlocksCount, 0);
            Assert.AreEqual(result.Blocks.Count, 0);           
        }

        /// <summary>
        /// Parser test for empty file.
        /// </summary>
        [TestMethod]
        public async Task ParserTest_ForEmptyFile()
        {
            const string text =@"";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);
            // should not throw an exception
        }

        /// <summary>
        /// Parser test for bad file.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public async Task ParserTest_ForBadFile()
        {
            const string text = @"!@#$%^&*()";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);
        }

        /// <summary>
        /// Parser test for not correct file.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public async Task ParserTest_ForNotCorrectFile()
        {
            const string text =
@"8 2
2 test
0 1
0 1
1 1
3 4
1 1 1
1 1 1
1 1 1
1 1 1";

            var file = new StringReader(text);
            var result = await parser.LoadData(file);
        }
    }
}
