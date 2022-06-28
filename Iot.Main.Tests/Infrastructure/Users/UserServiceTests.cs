using System.Threading.Tasks;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Users;

public class UserServiceTests : BaseTestCase
{
    private readonly IUserService _userService;
    private CreateUserRequest _createRequest;
    private UserData _userData;

    public UserServiceTests(IEFContext eFContext, ICurrentUserService currentUserService, ITestOutputHelper outputHelper, IUserService userService) : base(eFContext, currentUserService, outputHelper)
    {
        _userService = userService;
    }

    [Fact]
    public async Task CreateUserTest()
    {
        await RunSteps(
            Given_the_company,
            Given_the_admin_role,
            Given_the_authenticated_user,
            Given_the_create_request,
            When_user_created,
            Then_user_data_is_filled
        );
    }

    private Task Given_the_create_request()
    {
        _createRequest = new CreateUserRequest
        {
            FirstName = "test",
            LastName = "test",
            Login = "test@test.test",
            Password = "1234",
            RoleId = AdminRole.Id,
            ConfrmPassword = "1234"
        };

        return Task.CompletedTask;
    }

    private async Task When_user_created()
    {
        _userData = await _userService.Create(_createRequest);
    }

    private Task Then_user_data_is_filled()
    {
        Assert.NotNull(_userData);
        Assert.Equal(_createRequest.FirstName, _userData.FirstName);
        Assert.Equal(_createRequest.LastName, _userData.LastName);
        Assert.Equal(_createRequest.Login, _userData.Login);

        return Task.CompletedTask;
    }
}
