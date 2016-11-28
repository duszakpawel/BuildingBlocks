using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Exceptions;
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
        private IAlgorithmProvider _algorithmProvider;

        /// <summary>
        /// Setup method invoked before every test method in AlgorithmProviderTests class
        /// </summary>
        [SetUp]
        public void Init()
        {
            _algorithmProvider = new AlgorithmProvider(new BlockLogicProvider(), new EvaluateFunctionProvider());
        }

        /// <summary>
        /// sample execute method test for correct data
        /// </summary>
        /// <returns></returns>
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
                    Content = new[,] { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
                    WellHeight = 15,
                    LastBlock = null
                }
            };

            var result = await _algorithmProvider.Execute(simulations, k, step);

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Density, "1,00");
            Assert.AreEqual(result[0].CanvasChildren.Count, 2);
            Assert.AreEqual(result[0].AvailableBlocks.Count, 0);
            Assert.AreEqual(result[0].Content[0, 14], 1);
            Assert.AreEqual(result[0].Content[1, 14], 1);
        }

        /// <summary>
        /// sample execute method test for correct data
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ExecuteTest_ForSampleCorrectData2()
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
                            Content = new[,] { {true, true}, {true, false} },
                            Height = 2,
                            Id = 2,
                            Width = 2,
                            Quantity = 1
                        }
                    },
                    Content = new[,] { {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1} },
                    WellHeight = 15,
                    LastBlock = null
                }
            };

            var result = await _algorithmProvider.Execute(simulations, k, step);

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Density, "1,00");
            Assert.AreEqual(result[0].CanvasChildren.Count, 6);
            Assert.AreEqual(result[0].AvailableBlocks.Count, 0);
            Assert.AreEqual(result[0].Content[0, 28], 2);
            Assert.AreEqual(result[0].Content[0, 29], 2);
            Assert.AreEqual(result[0].Content[1, 28], 2);
            Assert.AreEqual(result[0].Content[2, 29], 1);
            Assert.AreEqual(result[0].Content[2, 28], 1);
            Assert.AreEqual(result[0].Content[2, 29], 1);
        }

        /// <summary>
        /// sample execute method test for not correct data
        /// </summary>
        /// <returns></returns>
        [Test]
        public void ExecuteTest_ForSampleNotCorrectData()
        {
            const int step = -1;
            const int k = -1;
            var simulations = new List<Simulation>
            {
                new Simulation
                {
                    AvailableBlocks = null,
                    Content = null,
                    WellHeight = -1,
                    LastBlock = null
                }
            };

            Assert.That(async () => await _algorithmProvider.Execute(simulations, k, step), Throws.TypeOf<AlgorithmLogicException>());
        }
    }
}