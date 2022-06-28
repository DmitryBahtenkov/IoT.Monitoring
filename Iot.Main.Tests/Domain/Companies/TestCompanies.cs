using System.Collections.Generic;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Tests.Domain.Alerts;
using Iot.Main.Tests.Domain.Constraints;
using Iot.Main.Tests.Domain.Roles;
using Iot.Main.Tests.Domain.Users;

namespace Iot.Main.Tests.Domain.Companies;

public static class TestCompanies
{
    public static Company CompanyWithRole => new Company("name", "description")
    {
        Id = 1,
        Roles = new List<Role> { new Role("test", new Company("name", "description")) { Id = 1 } },
    };

    public static Company CompanyWithUser => new Company("name", "description")
    {
        Id = 1,
        Roles = new List<Role> { new Role("test", new Company("name", "description")) { Id = 1 } },
        Users = new List<User> { TestUsers.UserWithoutCompany }
    };

    public static Company CompanyWithDevice => new Company("name", "description")
    {
        Id = 1,
        Devices = new List<Device> { new Device("token", "name", TestConstraints.Constraint, CompanyWithUser) },
        Constraints = new List<Constraint> { new Constraint("cnam", CompanyWithUser) { Id = 1 } },
        Alerts = new List<Alert> { TestAlerts.Alert }
    };
}
