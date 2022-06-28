using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Tests.Domain.Companies;
using Iot.Main.Tests.Domain.Constraints;

namespace Iot.Main.Tests.Domain.Devices;

public static class TestDevices
{
    public static Device Device => new Device("token", "name", TestConstraints.Constraint, TestCompanies.CompanyWithDevice);
}
