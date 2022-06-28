using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Constraints
{
    public partial class ConstraintTests
    {
        [Fact]
        public void CheckCorrectInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 11,
                Humidity = 11,
                Pressure = 11
            };

            constraint.CheckInputData(input);
        }

        [Fact]
        public void CheckMinTempInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = -11,
                Humidity = 11,
                Pressure = 11
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Температура ниже нормы");
        }

        [Fact]
        public void CheckMaxTempInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 111,
                Humidity = 11,
                Pressure = 11
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Температура выше нормы");
        }

        [Fact]
        public void CheckMinPressureInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 11,
                Humidity = 11,
                Pressure = -11
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Давление ниже нормы");
        }

        [Fact]
        public void CheckMaxPressureInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 11,
                Humidity = 11,
                Pressure = 111
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Давление выше нормы");
        }

        [Fact]
        public void CheckMinHumidityInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 11,
                Humidity = -11,
                Pressure = 11
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Влажность ниже нормы");
        }

        [Fact]
        public void CheckMaxHumidityInputTest()
        {
            var constraint = TestConstraints.FullConstraint;
            var input = new InputData
            {
                Temp = 11,
                Humidity = 111,
                Pressure = 11
            };

            var ex = Assert.Throws<DataAlertException>(() => constraint.CheckInputData(input));

            Assert.Contains(ex.Errors, x => x is "Влажность выше нормы");
        }
    }
}