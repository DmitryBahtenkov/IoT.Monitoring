using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Infrastructure.Builders;
using Xunit;

namespace Iot.Main.Tests.Infrastructure.Influx
{
    public class FluxQueryBuilderTests
    {
        private readonly string _validQuery = "from(bucket: \"main\")" +
                    "|> range(start: 0)" +
                    "|> filter(fn: (r) => r[\"_field\"] == \"Humidity\" or r[\"_field\"] == \"Pressure\" or r[\"_field\"] == \"Temp\")" +
                    $"|> filter(fn: (r) => r[\"Device\"] == \"{1234}\")" +
                    "|> pivot(rowKey: [\"_time\"], columnKey: [\"_field\"], valueColumn: \"_value\")" +
                    "|> yield(name: \"mean\")";
        

        [Fact]
        public void TestName()
        {
            var builder = new FluxQueryBuilder();
            
            var query = builder
                .FromBucket("main")
                .WithNullRange()
                .WithFields(nameof(InputData.Humidity), nameof(InputData.Pressure), nameof(InputData.Temp))
                .WithEqual("Device", "1234")
                .WithPivot("_time", "_field", "_value")
                .BuildYield("mean");

            Assert.Equal(_validQuery, query);
        }
    }
}