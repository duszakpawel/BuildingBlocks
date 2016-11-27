using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.Models.Models;
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
        public async Task ExecuteTest_ForSampleCorrectData()
        {
            const int step = 1;
            const int k = 1;
            var simulations = new List<Simulation>
            {
                new Simulation
                {
                    AvailableBlocks = new List<Block>
                    {
                        new Block
                        {
                            Content = new[,] { {true, true}},
                            Height = 1,
                            Id = 1,
                            Width = 2,
                            Quantity = 1
                        }
                    },
                    Content = new[,] { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
                    WellHeight = 2,
                    LastBlock = null
                }
            };

            var result = await _algorithmSolver.Execute(simulations, k, step);

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Density, "1,00");
            Assert.AreEqual(result[0].CanvasChildren.Count, 2);
            Assert.AreEqual(result[0].AvailableBlocks.Count, 0);
            Assert.AreEqual(result[0].Content[0, 14], 1);
            Assert.AreEqual(result[0].Content[1, 14], 1);
        }
    }
}