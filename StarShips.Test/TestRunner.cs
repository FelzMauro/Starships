using Domain.Entities;
using Domain.Interfaces;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarShips.Test
{
    [TestClass]
    public class TestRunner : TestBase
    {

        private ICalculator _calculator;
        private IApiCaller _apiCaller;

        [TestInitialize]
        public void Setup()
        {
            _calculator = Substitute.For<ICalculator>();
            _apiCaller = Substitute.For<IApiCaller>();
        }

        [TestMethod]
        public void Test_runner_info()
        {
            _apiCaller.GetAll().Returns(new List<StartshipDTO>
            {
                new StartshipDTO { name = "Millennium Falcon", MGLT = "75", consumables= "2 months" }
            });

            var calculator = _container.GetInstance<ICalculator>();

            var runner = new Runner(calculator, _apiCaller);

            runner.Init();

            var result = runner.Run(1000000);

            var expected = new ResultDTO { Starship = new StartshipDTO {name = "Millennium Falcon" }, Stop = "9" };

            Assert.AreEqual(expected.Starship.name, result.FirstOrDefault().Starship.name);
            Assert.AreEqual(expected.Stop, result.FirstOrDefault().Stop);
        }

        [TestMethod]
        public void Test_runner_invalid_mglt()
        {
            _apiCaller.GetAll().Returns(new List<StartshipDTO>
            {
                new StartshipDTO { name = "Millennium Falcon", MGLT = "-1", consumables= "2 months" }
            });

            _calculator = _container.GetInstance<ICalculator>();

            var runner = new Runner(_calculator, _apiCaller);

            runner.Init();

            var result = runner.Run(1000000);

            var expected = new ResultDTO { Starship = new StartshipDTO { name = "Millennium Falcon" }, Stop = "9" };

            Assert.AreEqual(expected.Starship.name, result.FirstOrDefault().Starship.name);
            Assert.AreEqual(expected.Stop, result.FirstOrDefault().Stop);

        }

        [TestMethod]
        public void Test_runner_mock()
        {
            var distance = 1000000;
            var mglt = 75;

            _apiCaller.GetAll().Returns(new List<StartshipDTO>
            {
                new StartshipDTO { name = "Millennium Falcon", MGLT = "75", consumables= "2 months" }
            });

            _calculator.ToHours(2, "months").Returns(1460);
            _calculator.ToStops(distance, mglt, 1460).Returns(9);

            var runner = new Runner(_calculator, _apiCaller);

            runner.Init();

            var result = runner.Run(distance);

            string expected = "9";

            Assert.AreEqual(expected, result.FirstOrDefault().Stop);
        }

        [TestMethod]
        public void Test_runner_real_calculator()
        {
            var distance = 1000000;
            var mglt = 75;
            var consumables ="2 months";

            _apiCaller.GetAll().Returns(new List<StartshipDTO>
            {
                new StartshipDTO { name = "Millennium Falcon", MGLT = mglt.ToString(), consumables = consumables }
            });

            var calculator = _container.GetInstance<ICalculator>();

            var runner = new Runner(calculator, _apiCaller);

            runner.Init();

            var result = runner.Run(distance);

            string expected = "9";

            Assert.AreEqual(expected, result.FirstOrDefault().Stop);
        }

        [TestMethod]
        public void Test_runner_full_integration()
        {
            using (_container)
            {
                var distance = 1000000;

                // real instances
                var calculator = _container.GetInstance<ICalculator>();
                var starshipService = _container.GetInstance<IApiCaller>();

                var runner = new Runner( calculator, starshipService);

                runner.Init();

                var result = runner.Run(distance);

                Assert.AreEqual(36, result.Count);
            }
        }
    }
}
