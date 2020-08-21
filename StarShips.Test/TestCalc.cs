using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarShips.Test
{
    [TestClass]
    public class TestCalc : TestBase
    {
        private ICalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = _container.GetInstance<ICalculator>();
        }

        [TestMethod]
        public void Test_days()
        {
            var hours = _calculator.ToHours(6, "days");
            var exptected = 144;

            Assert.AreEqual(exptected, hours);
        }

        [TestMethod]
        public void Test_years()
        {
            var hours = _calculator.ToHours(3, "years");
            var exptected = 26280;

            Assert.AreEqual(exptected, hours);
        }

        [TestMethod]
        public void Test_empty()
        {
            var hours = _calculator.ToHours(0, "");
            var exptected = 0;

            Assert.AreEqual(exptected, hours);
        }

        [TestMethod]
        public void Test_null()
        {
            var hours = _calculator.ToHours(0, null);
            var exptected = 0;

            Assert.AreEqual(exptected, hours);
        }
    }
}
