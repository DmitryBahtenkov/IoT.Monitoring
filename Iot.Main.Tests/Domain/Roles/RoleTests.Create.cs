using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.RoleModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Tests.Domain.Companies;
using Xunit;

namespace Iot.Main.Tests.Domain.Roles
{
    public partial class RoleTests
    {
        [Fact]
        public void CreateValidRoleTest()
        {
            var request = new CreateRoleRequest
            {
                Name = "testrole"
            };

            var role = Role.Create(request, TestCompanies.CompanyWithRole);

            Assert.Equal(request.Name, role.Name);
        }

        [Fact]
        public void CreateDuplicateRoleTest()
        {
            var request = new CreateRoleRequest
            {
                Name = "test"
            };

            Assert.Throws<BusinessException>(() => Role.Create(request, TestCompanies.CompanyWithRole));
        }

        [Fact]
        public void CreateInvalidRoleTest()
        {
            var request = new CreateRoleRequest();

            var exception = Assert.Throws<ValidationException>(() => Role.Create(request, TestCompanies.CompanyWithRole));

            Assert.NotEmpty(exception.Result.Errors);
            Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Name));
        }
    }
}