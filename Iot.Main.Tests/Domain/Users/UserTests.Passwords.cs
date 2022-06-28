using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Tests.Domain.Roles;
using Xunit;

namespace Iot.Main.Tests.Domain.Users;

public partial class UserTests
{
    [Fact]
    public void GeneratePasswordTest()
    {
        var (hash, salt) = User.GeneratePassword("1234");

        Assert.NotEmpty(hash);
        Assert.NotEmpty(salt);
    }

    [Fact]
    public void CompareEqualPasswordTest()
    {
        var user = new User(
            "f", "f", "f", "f@f.f",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", TestRoles.Role, null);

        Assert.True(user.ComparePassword("1234"));
    }

    [Fact]
    public void CompareNotEqualPasswordTest()
    {
        var user = new User(
            "f", "f", "f", "f@f.f",
            "asas",
            "asas", TestRoles.Role, null);

        Assert.False(user.ComparePassword("1234"));
    }

        [Fact]
    public void ChangeValidPasswordTest()
    {
        var request = new ChangePasswordRequest
        {
            OldPassword = "1234",
            NewPassword = "12345",
            ConfirmPassword = "12345"
        };

        var user = TestUsers.FirstUser;
        
        user.ChangePassword(request);

        Assert.True(user.ComparePassword("12345"));
    }

    [Fact]
    public void ChangeInvalidPasswordTest()
    {
        var request = new ChangePasswordRequest
        {
            OldPassword = "1234",
            NewPassword = "12345",
            ConfirmPassword = "123456"
        };

        var user = TestUsers.FirstUser;
        
        Assert.Throws<ValidationException>(() => user.ChangePassword(request));   
    }
}
