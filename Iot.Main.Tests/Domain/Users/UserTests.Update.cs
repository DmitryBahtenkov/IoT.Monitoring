using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Users;

public partial class UserTests
{
    [Fact]
    public void UpdateValidUserTest()
    {
        var request = new UpdateUserRequest()
        {
            FirstName = "new f",
            LastName = "new l",
            MiddleName = "new m",
            RoleId = 1
        };

        var user = TestUsers.FirstUser;

        user.UpdateInfo(request);

        Assert.Equal(request.FirstName, user.FirstName);
        Assert.Equal(request.LastName, user.LastName);
        Assert.Equal(request.MiddleName, user.MiddleName);
        Assert.Equal(request.RoleId, user.RoleId);
    }

    [Fact]
    public void UpdateInvalidUserTest()
    {
        var request = new UpdateUserRequest();
        var user = TestUsers.FirstUser;

        var ex = Assert.Throws<ValidationException>(() => user.UpdateInfo(request));

        Assert.NotEmpty(ex.Result.Errors);
    }

    [Fact]
    public void UpdateUserWithNotExistedRole()
    {
        var request = new UpdateUserRequest()
        {
            FirstName = "new f",
            LastName = "new l",
            MiddleName = "new m",
            RoleId = 44
        };
        var user = TestUsers.UserWithCompanyAndRoles;

        Assert.Throws<BusinessException>(() => user.UpdateInfo(request));
    }
}
