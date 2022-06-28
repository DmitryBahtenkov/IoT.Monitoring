using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Tests.Domain.Companies;

namespace Iot.Main.Tests.Domain.Roles
{
    public static class TestRoles
    {
        public static Role Role => new Role("test", TestCompanies.CompanyWithRole) { Id = 1 };
    }
}