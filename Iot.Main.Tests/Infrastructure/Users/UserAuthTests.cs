using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Aggregates;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Users;

public class UserAuthTests : BaseTestCase
{
    private readonly IAuthService _authService;

    public UserAuthTests(
        IEFContext eFContext,
        ICurrentUserService currentUserService,
        IAuthService authService,
        ITestOutputHelper outputHelper) : base(eFContext, currentUserService, outputHelper)
    {
        _authService = authService;
    }

    private LoginRequest _request;
    private RegisterAggregate _registerAggregate;
    private UserData _userData;

    [Fact]
    public async Task ValidLoginTest()
    {
        await RunSteps(
            Given_the_company,
            Given_the_admin_role,
            Given_the_user,
            Given_the_valid_login_request,
            When_login_executed,
            Then_user_token_not_empty
        );
    }

    [Fact]
    public async Task RegisterUserTest()
    {
        await RunSteps(
            Given_the_register_aggregate,
            When_register_executed,
            Then_user_token_not_empty,
            Then_company_is_created,
            Then_role_is_created
        );
    }

    [Fact]
    public async Task InvalidLoginTest()
    {
        await RunSteps(
            Given_the_company,
            Given_the_admin_role,
            Given_the_user,
            Given_the_invalid_login_request,
            When_login_executed,
            Then_business_exception_was_thrown
        );
    }

    [Fact]
    public async Task NonExistedLoginTest()
    {
        await RunSteps(
            Given_the_company,
            Given_the_admin_role,
            Given_the_user,
            Given_the_non_existed_user_login_request,
            When_login_executed,
            Then_business_exception_was_thrown
        );
    }

    private async Task Given_the_user()
    {
        await _eFContext.Set<User>().AddAsync(TestUser);
        await _eFContext.SaveChangesAsync();
    }

    private Task Given_the_valid_login_request()
    {
        _request = new LoginRequest
        {
            Login = "f@f.f",
            Password = "1234"
        };

        return Task.CompletedTask;
    }

    private Task Given_the_non_existed_user_login_request()
    {
        _request = new LoginRequest
        {
            Login = "asas@a.asas",
            Password = "1234"
        };

        return Task.CompletedTask;
    }

    private Task Given_the_invalid_login_request()
    {
        _request = new LoginRequest
        {
            Login = "f@f.f",
            Password = "12345"
        };

        return Task.CompletedTask;
    }

    private async Task When_login_executed()
    {
        await WrapAction(async () => _userData = await _authService.Login(_request));
    }

    private Task Then_user_token_not_empty()
    {
        Assert.NotNull(_userData);
        Assert.Equal("f@f.f", _userData.Login);
        Assert.NotEmpty(_userData.Token);

        return Task.CompletedTask;
    }

    private Task Given_the_register_aggregate()
    {
        _registerAggregate = new()
        {
            FirstName = "test",
            LastName = "test",
            MiddleName = "test",
            Login = "f@f.f",
            Password = "1234",
            ConfrmPassword = "1234",
            CompanyDescription = "t",
            CompanyName = "t"
        };

        return Task.CompletedTask;
    }

    private async Task When_register_executed()
    {
        _userData = await _authService.Register(_registerAggregate);
    }

    private async Task Then_company_is_created()
    {
        var company = await _eFContext.Set<Company>()
            .FirstOrDefaultAsync(x => x.Name == _registerAggregate.CompanyName);
        Assert.NotNull(company);
    }

    private async Task Then_role_is_created()
    {
        var role = await _eFContext.Set<Role>()
            .FirstOrDefaultAsync(x => x.Name == "Администратор (t)");

        Assert.NotNull(role);
    }
}
