using System.Diagnostics.CodeAnalysis;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Exceptions;
using BuildingBlocks.BusinessLogic.Interfaces;
using NUnit.Framework;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    ///     Tests for evaluate function provider.
    /// </summary>
    [TestFixture]
    public class EvaluateFunctionProviderTests
    {
        private readonly IEvaluateFunctionProvider _evaluateFunctionProvider = new EvaluateFunctionProvider();

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard()
        {
            var board = new[,]
            {
                {0, 0, 0},
                {1, 0, 1},
                {1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 80);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard2()
        {
            var board = new[,]
            {
                {1, 0, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 77);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard3()
        {
            var board = new[,]
            {
                {1, 1, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 88);
        }

        /// <summary>
        ///     Evaluate function test for null board
        /// </summary>
        [Test]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void EvaluateTest_ForSampleNullBoard()
        {
            int[,] board = null;

            Assert.That(() => _evaluateFunctionProvider.Evaluate(board), Throws.TypeOf<EvaluateFunctionException>());
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard4()
        {
            var board = new[,]
            {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 94);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard5()
        {
            var board = new[,]
            {
                {0, 0, 0},
                {0, 0, 0},
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1},
                {1, 0, 1},
                {1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 91);
        }

        /// <summary>
        ///     Evaluate function test for empty board
        /// </summary>
        [Test]
        public void EvaluateTest_ForEmptyBoard()
        {
            var board = new[,]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 100);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard6()
        {
            var board = new[,]
            {
                {1, 1, 1, 0},
                {1, 1, 1, 1},
                {1, 1, 1, 0},
                {1, 1, 1, 1},
                {1, 0, 1, 0},
                {1, 1, 1, 1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 83);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard7()
        {
            var board = new[,]
            {
                {1, 1, 1, 0, 0},
                {1, 1, 1, 1, 0},
                {1, 1, 1, 0, 1},
                {1, 1, 1, 1, 1},
                {1, 0, 1, 0, 1},
                {1, 1, 1, 1, 0}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 76);
        }

        /// <summary>
        ///     Evaluate function test for sample correct board
        /// </summary>
        [Test]
        public void EvaluateTest_ForSampleCorrectBoard8()
        {
            var board = new[,]
            {
                {1},
                {1},
                {1},
                {1},
                {1},
                {1}
            };

            var result = _evaluateFunctionProvider.Evaluate(board);
            Assert.AreEqual(result, 100);
        }
    }
}