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
    }
}