using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;

namespace Iot.Main.Tests.Domain.Alerts
{
    public class TestAlerts
    {
        public static Alert Alert => new Alert("test@test.t", "test", new Company("test", "test")
        {
            Id = 1
        });
    }
}