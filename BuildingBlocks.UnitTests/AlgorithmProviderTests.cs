using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Interfaces;
using Moq;
using NUnit.Framework;

namespace BuildingBlocks.UnitTests
{
    /// <summary>
    ///     Tests for array extensions.
    /// </summary>
    [TestFixture]
    public class AlgorithmProviderTests
    {
        private IAlgorithmSolver _algorithmSolver;

        /// <summary>
        /// Setup method invoked before every test method in AlgorithmProviderTests class
        /// </summary>
        [SetUp]
        public void Init()
        {
            _algorithmSolver = new AlgorithmSolver(new BlockLogicProvider(), new EvaluateFunctionProvider());
        }

        [Test]
        public void ExecuteTest_ForSampleCorrectData()
        {
            
        }   
    }
}