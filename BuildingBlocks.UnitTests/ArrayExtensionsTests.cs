using System.Diagnostics.CodeAnalysis;
using BuildingBlocks.BusinessLogic.Extension_methods;
using NUnit.Framework;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    ///     Tests for array extensions.
    /// </summary>
    [TestFixture]
    public class ArrayExtensionsTests
    {
        /// <summary>
        ///     Has equal content method test for equal length of tables and equal content of int type
        /// </summary>
        [Test]
        public void HasEqualContentTest_ForEqualLengthOfTablesAndEqualContent()
        {
            int[,] tab1 = { { 1, 2 }, { 2, 3 } };
            var tab2 = (int[,])tab1.Clone();

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, true);
        }

        /// <summary>
        ///     Has equal content method test for not equal length of tables of int type
        /// </summary>
        [Test]
        public void HasEqualContentTest_ForNotEqualLengthOfTables()
        {
            int[,] tab1 = { { 1, 2 }, { 2, 3 } };
            int[,] tab2 = { { 1, 2 }, { 2, 3 }, { 3, 4 } };

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, false);
        }

        /// <summary>
        ///     Has equal content method test for equal length of tables and not equal content of int type
        /// </summary>
        [Test]
        public void HasEqualContentTest_ForEqualLengthOfTablesAndNotEqualContent()
        {
            int[,] tab1 = { { 1, 2 }, { 2, 3 } };
            int[,] tab2 = { { 1, 2 }, { 3, 4 } };

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, false);
        }

        /// <summary>
        ///     Has equal content method test for both null arrays of int type
        /// </summary>
        [Test]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void HasEqualContentTest_ForNullArrays()
        {
            int[,] tab1 = null;
            int[,] tab2 = null;

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, true);
        }

        /// <summary>
        ///     Has equal content method test for first array of int type equal to null
        /// </summary>
        [Test]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void HasEqualContentTest_ForFirstArrayEqualToNull()
        {
            int[,] tab1 = null;
            int[,] tab2 = { {1, 2}, {2, 3} };

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, false);
        }

        /// <summary>
        ///     Has equal content method test for second array of int type equal to null
        /// </summary>
        [Test]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void HasEqualContentTest_ForSecondArrayEqualToNull()
        {
            int[,] tab1 = { { 1, 2 }, { 2, 3 } };
            int[,] tab2 = null;

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, false);
        }

        /// <summary>
        ///     Has equal content method test for equal length of tables and equal content of object type
        /// </summary>
        [Test]
        public void HasEqualContentTest_ForEqualLengthOfTablesAndEqualContentOfObjectType()
        {
            object[,] tab1 = { { 1, 2 }, { 2, 3 } };
            var tab2 = (object[,])tab1.Clone();

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, true);
        }

        /// <summary>
        ///     Has equal content method test for equal length of tables and equal content of int type but not equal dimensions length
        /// </summary>
        [Test]
        public void HasEqualContentTest_ForEqualLengthOfTablesAndEqualContent2()
        {
            int[,] tab1 = { { 1, 2, 3 }, { 2, 3, 4 } };
            int[,] tab2 = { {1, 2}, {3, 2}, {3, 4} };

            var result = tab1.HasEqualContent(tab2);

            Assert.AreEqual(result, false);
        }
    }
}