using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Tests.Domain.Companies;
using Iot.Main.Tests.Domain.Roles;
using Xunit;

namespace Iot.Main.Tests.Domain.Users;

public partial class UserTests
{
    [Fact]
    public void CreateValidUserTest()
    {
        var request = new CreateUserRequest
        {
            FirstName = "F",
            LastName = "L",
            Login = "l@l.l",
            Password = "1234",
            ConfrmPassword = "1234",
            RoleId = 1
        };

        var user = User.Create(request, TestRoles.Role, TestCompanies.CompanyWithRole);

        Assert.NotNull(user);
        Assert.Equal(request.FirstName, user.FirstName);
        Assert.Equal(request.LastName, user.LastName);
        Assert.Equal(request.Login, user.Login);
        Assert.Equal(request.FirstName, user.FirstName);
        Assert.True(User.ComparePassword(request.Password, user));
    }

    [Fact]
    public void CreateInvalidUserTest()
    {
        var request = new CreateUserRequest();

        var exception = Assert.Throws<ValidationException>(() => User.Create(request, TestRoles.Role, TestCompanies.CompanyWithRole));

        Assert.NotEmpty(exception.Result.Errors);
        Assert.Contains(exception.Result.Errors, x => x.Key is "FirstName");
        Assert.Contains(exception.Result.Errors, x => x.Key is "LastName");
        Assert.Contains(exception.Result.Errors, x => x.Key is "Login");
    }

    [Fact]
    public void CreateUserWithNotExistedRoleTest()
    {
        var request = new CreateUserRequest
        {
            FirstName = "F",
            LastName = "L",
            Login = "l@l.l",
            Password = "1234",
            ConfrmPassword = "1234",
            RoleId = 22
        };

        Assert.Throws<BusinessException>(() => User.Create(request, new Role("role", new Company("a", "a")) { Id = 11}, TestCompanies.CompanyWithRole));
    }

    [Fact]
    public void CreateDuplicateUser()
    {
        var request = new CreateUserRequest
        {
            FirstName = "F",
            LastName = "L",
            Login = "f@f.f",
            Password = "1234",
            ConfrmPassword = "1234",
            RoleId = 22
        };

        Assert.Throws<BusinessException>(() => User.Create(request, new Role("role", TestCompanies.CompanyWithUser), TestCompanies.CompanyWithUser));
    }
}
