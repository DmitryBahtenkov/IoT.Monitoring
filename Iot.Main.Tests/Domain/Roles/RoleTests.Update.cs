using Iot.Main.Domain.Models.RoleModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Roles;

public partial class RoleTests
{
    [Fact]
    public void UpdateValidRoleTest()
    {
        var role = TestRoles.Role;
        var request = new UpdateRoleRequest
        {
            Name = "newname"
        };

        role.Update(request);

        Assert.Equal(request.Name, role.Name);
    }

    [Fact]
    public void UpdateInvalidRoleTest()
    {
        var role = TestRoles.Role;
        var request = new UpdateRoleRequest();

        var exception = Assert.Throws<ValidationException>(() => role.Update(request));

        Assert.NotEmpty(exception.Result.Errors);
        Assert.Contains(exception.Result.Errors, x => x.Key == nameof(request.Name));
    }

    [Fact]
    public void UpdateDuplicateRoleTest()
    {
        var role = TestRoles.Role;
        var request = new UpdateRoleRequest
        {
            Name = "test"
        };

        Assert.Throws<BusinessException>(() => role.Update(request));
    }
}
